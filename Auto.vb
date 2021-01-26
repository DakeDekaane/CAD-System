Public Class Auto
    Public entidad As AcadBlockReference    'Bloque que representa el automovil
    Public vialidad As AcadLine             'Vialidad sobre la cual viaja el automovil
    Public velocidad As Double              'Velocidad a la que viaja el automovil
    Public existe As Boolean                'Determina si el automovil se muestra o no
    Public longitud As Double               'Longitud del automovil (en mm)

    Public Sub New(_entidad As AcadBlockReference, _vialidad As AcadLine, _velocidad As Double, _longitud As Double)
        entidad = _entidad
        vialidad = _vialidad
        velocidad = _velocidad
        existe = True
        longitud = _longitud
    End Sub

    Public Sub Simular()

        Dim nuevasVialidades As List(Of AcadLine)
        Dim nuevaVialidad As AcadLine
        Dim semaforo As AcadEntity
        Dim estadoSemaforo As String
        Dim p1() As Double
        Dim p2() As Double
        Dim autoProximo As AcadEntity
        Dim indice As New Random()

        'Se buscan semáforos
        semaforo = BuscarSemaforoProximo(velocidad + 1000)
        'Si se encuentra uno
        If Not IsNothing(semaforo) Then
            estadoSemaforo = ObtenerPropiedad(semaforo, "Estado")
            'Si se encuentra con una luz roja
            If estadoSemaforo = "Rojo" Then
                'Se "detiene" el automovil (salimos de la funcion)
                Return
            End If
        End If

        'Se buscan autos próximos
        autoProximo = BuscarAutoProximo(velocidad + 500)
        'Si se encuentra uno
        If Not IsNothing(autoProximo) Then
            'Se "detiene" el automovil (salimos de la función)
            Return
        End If

        'Se buscan nuevas vialidades
        nuevasVialidades = BuscarVialidades(velocidad)
        'Si no hay nuevas vialidades, quiere decir que solo se encuentra la vialidad sobre la cual se mueve el auto
        If nuevasVialidades.Count = 0 Then
            'Se verifica si podemos continuar sobre la vialidad actual
            If Dist2Points(entidad.InsertionPoint, vialidad.EndPoint) > velocidad Then
                'Entonces se mueve el auto desde su punto de inserción una distancia dada por la velocidad
                p1 = entidad.InsertionPoint
                p2 = utility.PolarPoint(p1, vialidad.Angle, velocidad)
                entidad.Move(p1, p2)
                existe = True
                'Si no podemos continuar
            Else
                'Se elimina el automovil
                existe = False
            End If

            'En caso contrario, analizamos las vialidades nuevas encontradas
        Else
            'Se elige una vialidad posible al azar
            nuevaVialidad = nuevasVialidades(indice.Next(0, nuevasVialidades.Count))
            p1 = entidad.InsertionPoint
            p2 = nuevaVialidad.StartPoint
            'Se mueve y se rota de acuerdo a la nueva vialidad
            entidad.Move(p1, p2)
            entidad.Rotate(entidad.InsertionPoint, nuevaVialidad.Angle - vialidad.Angle)
            p2 = utility.PolarPoint(entidad.InsertionPoint, nuevaVialidad.Angle, longitud)
            entidad.Move(entidad.InsertionPoint, p2)
            'Se configura la nueva vialidad por la cual va a viajar el auto
            ConfigurarPropiedad(entidad, "Vialidad", nuevaVialidad.Handle)
            vialidad = nuevaVialidad
            existe = True
        End If

        If Not existe Then
            entidad.Delete()
        End If
    End Sub


    Public Function BuscarVialidades(radio As Double) As List(Of AcadLine)

        Dim corners() As Double
        Dim objectSet As AcadSelectionSet
        Dim vialidades As List(Of AcadLine) = New List(Of AcadLine)

        corners = GeneraCoordenadasCirculo(entidad.InsertionPoint, radio, 0, 360, 10)
        objectSet = CrearConjuntoVacio(document, "IDLE")

        If Not IsNothing(objectSet) Then
            'Se añaden los objetos dentro del rectángulo al conjunto
            objectSet.SelectByPolygon(AcSelect.acSelectionSetCrossingPolygon, corners)
            For Each element In objectSet
                'Si encuentra una vialidad, es diferente a la vialidad por la cual viaja y además puede viajar sobre la nueva vialidad
                If ObtenerPropiedad(element, "Tipo") = "Vialidad" AndAlso element.Handle <> vialidad.Handle AndAlso SonIguales(vialidad.EndPoint, element.StartPoint) Then
                    'Añade la vialidad a la lista
                    vialidades.Add(element)
                End If
            Next
            objectSet.Delete()
        End If

        Return vialidades
    End Function

    Public Function BuscarAutoProximo(radio As Double) As AcadBlockReference
        Dim corners() As Double
        Dim objectSet As AcadSelectionSet
        Dim autoProximo As AcadEntity = Nothing

        corners = GeneraCoordenadasCirculo(entidad.InsertionPoint, radio, 0, 360, 10)
        objectSet = CrearConjuntoVacio(document, "IDLE")

        If Not IsNothing(objectSet) Then
            'Se añaden los objetos dentro del rectángulo al conjunto
            objectSet.SelectByPolygon(AcSelect.acSelectionSetCrossingPolygon, corners)
            For Each element In objectSet
                'Si encuentra un auto distinto a el mismo
                If ObtenerPropiedad(element, "Tipo") = "Auto" AndAlso element.Handle <> entidad.Handle Then
                    'Se asigna a la variable de retorno
                    autoProximo = element
                    Exit For
                End If
            Next
            objectSet.Delete()
        End If

        Return autoProximo
    End Function

    Public Function BuscarSemaforoProximo(radio As Double) As AcadEntity
        Dim corners() As Double
        Dim objectSet As AcadSelectionSet
        Dim semaforo As AcadEntity = Nothing

        corners = GeneraCoordenadasCirculo(entidad.InsertionPoint, radio, 0, 360, 10)
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

End Class
