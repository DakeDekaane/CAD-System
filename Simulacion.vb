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

    Public Function PrepararAutos() As List(Of Auto)
        Dim autos = New List(Of Auto)
        Dim auto As Auto
        Dim entidad As AcadEntity
        Dim vialidad As AcadLine
        Dim velocidad As Double = 1000
        Dim longitud As Double = 4000
        Dim success = False
        While Not success
            Try
                autos.Clear()
                For Each element In document.ModelSpace
                    If ObtenerPropiedad(element, "Tipo") = "Auto" Then
                        entidad = element
                        vialidad = document.HandleToObject(ObtenerPropiedad(element, "Vialidad"))
                        auto = New Auto(entidad, vialidad, velocidad, longitud)
                        autos.Add(auto)

                    End If
                Next
                success = True
            Catch ex As System.Runtime.InteropServices.COMException
                success = False
            End Try
        End While

        Return autos
    End Function
    Public Function PrepararSemaforos() As List(Of AcadEntity)
        Dim semaforos = New List(Of AcadEntity)
        Dim success = False
        While Not success
            Try
                semaforos.Clear()
                For Each element In document.ModelSpace
                    If ObtenerPropiedad(element, "Tipo") = "Semaforo" Then
                        semaforos.Add(element)
                    End If
                Next
                success = True
            Catch ex As System.Runtime.InteropServices.COMException
                success = False
            End Try
        End While

        Return semaforos
    End Function

    Public Sub Simular(autos As List(Of Auto), semaforos As List(Of AcadEntity))
        Dim success As Boolean
        For Each auto In autos
            success = False
            If auto.existe Then
                While Not success
                    Try
                        auto.Simular()
                        success = True
                    Catch ex As System.Runtime.InteropServices.COMException
                        success = False
                    End Try
                End While
            End If
        Next
        For Each semaforo In semaforos
            success = False
            While Not success
                Try
                    SimularSemaforo(semaforo)
                    success = True
                Catch ex As System.Runtime.InteropServices.COMException
                    success = False
                End Try
            End While
        Next
    End Sub

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
        Dim puntoMedio() As Double
        Dim lineaFlecha1 As AcadLine
        Dim anguloFlecha1 As Double
        Dim lineaFlecha2 As AcadLine
        Dim anguloFlecha2 As Double
        Dim longitudFlecha As Double = 2000
        Dim p1() As Double
        Dim p2() As Double

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
        Dim puntoTmp() As Double

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
