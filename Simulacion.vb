Module Simulacion
    Public Sub AgregarAuto()
        Dim bloqueAuto As AcadBlockReference
        Dim puntoInsercion(0 To 2) As Double
        Dim anguloInsercion As Double
        Dim vialidad As AcadLine = Nothing

        AppActivateAutoCAD()

        'Try
        utility.GetEntity(vialidad, puntoInsercion, "Selecciona una vialidad")
            'Seleccionamos vialidad
            If ObtenerPropiedad(vialidad, "Tipo") = "Vialidad" Then
                'Obtenemos el ángulo de la vialidad
                anguloInsercion = vialidad.Angle - ConvertDeg2Rad(180)
                'Se inserta el automovil
                bloqueAuto = document.ModelSpace.InsertBlock(puntoInsercion, "auto", 1, 1, 1, anguloInsercion)
                'Se configura el tipo y la vialidad sobre la cual viaja
                ConfigurarPropiedad(bloqueAuto, "Tipo", "Auto")
                ConfigurarPropiedad(bloqueAuto, "Vialidad", vialidad.Handle)
            End If

            'Catch ex As Exception
        'MsgBox(ex.Message, MsgBoxStyle.Information, "CAD")
        'End Try
    End Sub

    Public Sub Simular()
        Dim element As AcadEntity
        Dim tipo As String
        Dim auto As AcadBlockReference
        Dim semaforo As AcadEntity

        AppActivateAutoCAD()

        'Se analizan todos los elementos de tipo auto y semáforo
        For Each element In document.ModelSpace
            tipo = ObtenerPropiedad(element, "Tipo")
            'Try
            '---
            Dim bandaid As Boolean = False
            While Not bandaid
                Try
                    If tipo = "Auto" Then
                        auto = element
                        SimularAuto(auto)
                    ElseIf tipo = "Semaforo" Then
                        semaforo = element
                        SimularSemaforo(semaforo)
                    End If
                    bandaid = True
                Catch ex As System.Runtime.InteropServices.COMException
                    bandaid = False
                End Try
            End While
            '---
            'Catch ex As Exception
            'MsgBox(ex.Message, MsgBoxStyle.Information)
            'End Try
        Next
    End Sub

    Public Sub SimularAuto(auto As AcadBlockReference)
        Dim vialidad As AcadLine
        Dim nuevaVialidad() As AcadLine
        Dim semaforo As AcadEntity
        Dim estadoSemaforo As String
        Dim numeroVialidades As Integer
        Dim p1(0 To 2) As Double
        Dim p2(0 To 2) As Double
        Dim velocidad = 1000 'mm/s'
        Dim longitudAuto As Double = 4000
        Dim borrar As Boolean = True
        Dim autoProximo As AcadEntity
        'Se buscan semáforos
        semaforo = BuscaSemaforo(auto)
        If Not IsNothing(semaforo) Then
            estadoSemaforo = ObtenerPropiedad(semaforo, "Estado")
            'Si se encuentra con una luz roja
            If estadoSemaforo = "Rojo" Then
                'Se "detiene" el automovil (salimos de la funcion)
                Return
            End If
        End If

        'Se buscan autos próximos
        autoProximo = BuscaAutos(auto)
        'Si se encuentra uno
        If Not IsNothing(autoProximo) Then
            'Se "detiene" el automovil (salimos de la función)
            Return
        End If

        vialidad = document.HandleToObject(ObtenerPropiedad(auto, "Vialidad"))
        nuevaVialidad = BuscaVialidad(auto, numeroVialidades)
        'Si no hay nuevas vialidades, quiere decir que solo se encuentra la vialidad sobre la cual se mueve el auto
        If numeroVialidades = 0 Then
            'Entonces se mueve el auto desde su punto de inserción una distancia dada por la velocidad
            p1 = auto.InsertionPoint
            p2 = utility.PolarPoint(p1, vialidad.Angle, velocidad)
            auto.Move(p1, p2)
            borrar = False
            'En caso contrario, analizamos las vialidades nuevas encontradas
        Else
            For Each nueva In nuevaVialidad
                'Si el punto final de la vialidad actual es igual al punto inicial de la nueva vialidad
                'seguimos por la nueva linea
                If Not IsNothing(nueva) AndAlso SonIguales(vialidad.EndPoint, nueva.StartPoint) Then
                    p1 = auto.InsertionPoint
                    p2 = nueva.StartPoint
                    'Se configura la nueva vialidad por la cual va a viajar el auto
                    ConfigurarPropiedad(auto, "Vialidad", nueva.Handle)
                    'Se mueve y se rota de acuerdo a la nueva vialidad
                    auto.Move(p1, p2)
                    auto.Rotate(auto.InsertionPoint, nueva.Angle - vialidad.Angle)
                    p2 = utility.PolarPoint(auto.InsertionPoint, nueva.Angle, longitudAuto)
                    auto.Move(auto.InsertionPoint, p2)
                    borrar = False
                    Exit For
                End If
            Next
        End If
        If borrar Then
            auto.Delete()
        End If
    End Sub

    Public Function BuscaVialidad(auto As AcadBlockReference, ByRef numero As Integer) As AcadLine()
        Dim corners() As Double
        Dim radio = 1000 'mm'
        Dim objectSet As AcadSelectionSet
        Dim vialidades(0 To 5) As AcadLine
        numero = 0

        corners = GeneraCoordenadasCirculo(auto.InsertionPoint, radio, 0, 360, 10)
        objectSet = CrearConjuntoVacio(document, "IDLE")

        If Not IsNothing(objectSet) Then
            'Se añaden los objetos dentro del rectángulo al conjunto
            objectSet.SelectByPolygon(AcSelect.acSelectionSetCrossingPolygon, corners)
            For Each element In objectSet
                'Si encuentra una vialidad y es diferente a la vialidad por la cual viaja
                If ObtenerPropiedad(element, "Tipo") = "Vialidad" AndAlso element.Handle <> ObtenerPropiedad(auto, "Vialidad") Then
                    'Devuelve la nueva vialidad para seguir por ella
                    vialidades(numero) = element
                    numero += 1
                End If
            Next
            objectSet.Delete()
        End If
        Return vialidades
    End Function

    Public Function BuscaAutos(auto As AcadBlockReference) As AcadEntity
        Dim corners() As Double
        Dim radio = 1500 'mm'
        Dim objectSet As AcadSelectionSet
        Dim autoProximo As AcadEntity = Nothing
        corners = GeneraCoordenadasCirculo(auto.InsertionPoint, radio, 0, 360, 10)
        objectSet = CrearConjuntoVacio(document, "IDLE")

        If Not IsNothing(objectSet) Then
            'Se añaden los objetos dentro del rectángulo al conjunto
            objectSet.SelectByPolygon(AcSelect.acSelectionSetCrossingPolygon, corners)
            For Each element In objectSet
                'Si encuentra una semaforo
                If ObtenerPropiedad(element, "Tipo") = "Auto" AndAlso element.Handle <> auto.Handle Then
                    'Se asigna a la variable de retorno
                    autoProximo = element
                    Exit For
                End If
            Next

            objectSet.Delete()
        End If
        Return autoProximo
    End Function

    Public Function BuscaSemaforo(auto As AcadBlockReference) As AcadEntity
        Dim corners() As Double
        Dim radio = 2000 'mm'
        Dim objectSet As AcadSelectionSet
        Dim semaforo As AcadEntity = Nothing
        corners = GeneraCoordenadasCirculo(auto.InsertionPoint, radio, 0, 360, 10)
        objectSet = CrearConjuntoVacio(document, "IDLE")

        If Not IsNothing(objectSet) Then
            'Se añaden los objetos dentro del rectángulo al conjunto
            objectSet.SelectByPolygon(AcSelect.acSelectionSetCrossingPolygon, corners)
            For Each element In objectSet
                'Si encuentra una semaforo
                If ObtenerPropiedad(element, "Tipo") = "Semaforo" Then
                    'Se asigna a la variable de retorno
                    semaforo = element
                    Exit For
                End If
            Next
            objectSet.Delete()
        End If
        Return semaforo
    End Function

    Public Sub SimularSemaforo(semaforo As AcadEntity)
        Dim estado As String
        Dim contador As Integer

        'Se obtienen el color y el contador del semaforo
        estado = ObtenerPropiedad(semaforo, "Estado")
        contador = Convert.ToInt32(ObtenerPropiedad(semaforo, "Contador"))

        'Se modifica el contador
        contador -= 1

        'Se verifica si hay un cambio de estado
        'En caso afirmativo, se cambia el estado y el color
        If contador = 0 Then
            If estado = "Verde" Then
                estado = "Rojo"
                semaforo.color = ACAD_COLOR.acRed
            ElseIf estado = "Rojo" Then
                estado = "Verde"
                semaforo.color = ACAD_COLOR.acGreen
            End If
            'Aplicando las propiedades al objeto
            ReiniciarSemaforo(semaforo, estado)
        Else
            'Se guarda el contador modificado en el objeto
            ConfigurarPropiedad(semaforo, "Contador", contador.ToString())
        End If

    End Sub

    Public Sub ReiniciarSemaforo(semaforo As AcadEntity, estado As String)
        Dim tiempo As Integer = 10
        'Se configuran el nuevo color y se reinicia el contador
        ConfigurarPropiedad(semaforo, "Estado", estado)
        ConfigurarPropiedad(semaforo, "Contador", tiempo.ToString)
        'Se cambia el color
        If estado = "Verde" Then
            semaforo.color = ACAD_COLOR.acGreen
        ElseIf estado = "Rojo" Then
            semaforo.color = ACAD_COLOR.acRed
        End If
    End Sub

    Public Sub ReiniciarSemaforo(estado As String)
        Dim entidad As AcadEntity
        AppActivateAutoCAD()

        'Se selecciona una entidad y se verifica que sea un semáforo
        entidad = ObtenerEntidad("Selecciona un objeto")
        If Not IsNothing(entidad) AndAlso ObtenerPropiedad(entidad, "Tipo") = "Semaforo" Then
            'Se reinicia el semaforo
            ReiniciarSemaforo(entidad, estado)
        End If
    End Sub

    Public Sub ColocarSentido()
        Dim entidad As AcadEntity
        Dim linea As AcadLine
        Dim puntoMedio(0 To 2) As Double
        Dim lineaFlecha1 As AcadLine
        Dim anguloFlecha1 As Double
        Dim lineaFlecha2 As AcadLine
        Dim anguloFlecha2 As Double
        Dim longitudFlecha As Double = 2000
        Dim p1(0 To 2) As Double
        Dim p2(0 To 2) As Double

        AppActivateAutoCAD()

        'Se selecciona una linea y se verifica que sea una vialidad
        entidad = ObtenerEntidad("Selecciona una vialidad")
        If ObtenerPropiedad(entidad, "Tipo") = "Vialidad" Then
            linea = entidad

            'Se calcula el punto medio de la linea y se trazan dos lineas para formar una flecha
            'que indica el sentido de la vialidad
            puntoMedio = utility.PolarPoint(linea.StartPoint, linea.Angle, linea.Length / 2)
            anguloFlecha1 = linea.Angle + ConvertDeg2Rad(135)
            anguloFlecha2 = anguloFlecha1 + ConvertDeg2Rad(90)
            p1 = utility.PolarPoint(puntoMedio, anguloFlecha1, longitudFlecha)
            p2 = utility.PolarPoint(puntoMedio, anguloFlecha2, longitudFlecha)
            lineaFlecha1 = document.ModelSpace.AddLine(puntoMedio, p1)
            lineaFlecha2 = document.ModelSpace.AddLine(puntoMedio, p2)
            lineaFlecha1.color = ACAD_COLOR.acYellow
            lineaFlecha2.color = ACAD_COLOR.acYellow

            'Se guardan los handle de las lineas para tener una referencia al momento de cambiar sentido
            ConfigurarPropiedad(linea, "Flecha1", lineaFlecha1.Handle)
            ConfigurarPropiedad(linea, "Flecha2", lineaFlecha2.Handle)
            ConfigurarPropiedad(lineaFlecha1, "Tipo", "Flecha")
            ConfigurarPropiedad(lineaFlecha2, "Tipo", "Flecha")
        End If

    End Sub

    Public Sub CambiarSentido()
        Dim entidad As AcadEntity
        Dim linea As AcadLine
        Dim lineaFlecha1 As AcadLine
        Dim lineaFlecha2 As AcadLine
        Dim puntoTmp(0 To 2) As Double

        AppActivateAutoCAD()

        'Se seleeciona una linea y se verifica que sea una vialidad
        entidad = ObtenerEntidad("Selecciona una vialidad")
        If ObtenerPropiedad(entidad, "Tipo") = "Vialidad" Then
            linea = entidad

            'Se obtienen las lineas que forman la flecha de dirección de la vialidad y se rotan
            lineaFlecha1 = document.HandleToObject(ObtenerPropiedad(linea, "Flecha1"))
            lineaFlecha2 = document.HandleToObject(ObtenerPropiedad(linea, "Flecha2"))
            lineaFlecha1.Rotate(lineaFlecha1.StartPoint, -ConvertDeg2Rad(90))
            lineaFlecha2.Rotate(lineaFlecha2.StartPoint, ConvertDeg2Rad(90))

            'Se invierten los puntos inicial y final de la linea
            puntoTmp = linea.StartPoint
            linea.StartPoint = linea.EndPoint
            linea.EndPoint = puntoTmp
        End If
    End Sub

End Module
