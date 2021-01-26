Module Utilidades
    Public Sub AppActivateAutoCAD()
        Dim AUTOCADWINDNAME As String
        Dim acadProcess() As Process = Process.GetProcessesByName("ACAD")
        Try
            AUTOCADWINDNAME = acadProcess(0).MainWindowTitle
            AppActivate(AUTOCADWINDNAME)
        Catch ex As Exception
            MsgBox("No se pudo activar AutoCAD" & ex.Message)
        End Try
    End Sub

    Public Function ObtenerEntidad(mensaje As String) As AcadEntity
        Dim entidad As AcadEntity = Nothing
        Dim p() As Double = Nothing

        Try
            utility.GetEntity(entidad, p, mensaje)
        Catch ex As Exception
            entidad = Nothing
        End Try

        Return entidad
    End Function
    Public Function CrearConjuntoVacio(document As AcadDocument, nombre As String) As AcadSelectionSet
        Dim limit As Integer
        Dim index As Integer
        Dim cObjects As AcadSelectionSet = Nothing
        nombre = nombre.Trim.ToUpper()
        'Try
        limit = document.SelectionSets.Count - 1
            For index = 0 To limit
                If document.SelectionSets.Item(index).Name = nombre Then
                    document.SelectionSets.Item(index).Delete()
                    Exit For
                End If
            Next
            cObjects = document.SelectionSets.Add(nombre)
        'Catch ex As Exception
        'MsgBox(ex.Message & "No se pudo crear conjunto vacio", MsgBoxStyle.Information, "CAD")
        'End Try
        Return cObjects
    End Function

    Public Function GeneraCoordenadasCirculo(p() As Double, radio As Double, anguloInicial As Double, anguloFinal As Double, pasos As Double) As Double()

        Dim angulo As Double
        Dim pN As Integer = 0
        Dim stepAngle As Double = (anguloFinal - anguloInicial) / pasos
        Dim puntos() As Double = Nothing
        Dim pPolar() As Double

        stepAngle = ConvertDeg2Rad(stepAngle)
        angulo = ConvertDeg2Rad(anguloInicial)

        For i = 1 To pasos
            pPolar = utility.PolarPoint(p, angulo, radio) '!!!'
            ReDim Preserve puntos(pN + 2)
            puntos(pN) = pPolar(0) : puntos(pN + 1) = pPolar(1) : puntos(pN + 2) = pPolar(2)
            angulo += stepAngle
            pN += 3

        Next
        Return puntos
    End Function

    Public Function ConvertDeg2Rad(anguloDeg As Double) As Double
        Return (anguloDeg * Math.PI) / 180
    End Function

    Public Function Dist2Line(punto() As Double, linea As AcadLine, ByRef cerca() As Double) As Double
        Dim t As Double
        Dim x1 = linea.StartPoint(0)
        Dim y1 = linea.StartPoint(1)
        Dim x2 = linea.EndPoint(0)
        Dim y2 = linea.EndPoint(1)
        Dim dx = x2 - x1
        Dim dy = y2 - y1

        t = ((punto(0) - x1) * dx + (punto(1) - y1) * dy) / (dx * dx + dy * dy)

        If t < 0 Then
            'dx = punto(0) - x1
            'dy = punto(1) - y1
            cerca(0) = x1
            cerca(1) = y1
        ElseIf t > 1 Then
            'dx = punto(0) - x2
            'dy = punto(1) - y2
            cerca(0) = x2
            cerca(1) = y2
        Else
            cerca(0) = x1 + t * dx
            cerca(1) = y1 + t * dy
            'dx = punto(0) - cerca(0)
            'dy = punto(1) - cerca(1)
        End If
        Return t
    End Function
    Public Function SonIguales(endPoint() As Double, startPoint() As Double) As Boolean
        Return endPoint(0) = startPoint(0) And endPoint(1) = startPoint(1)
    End Function


    Public Function Dist2Points(a() As Double, b() As Double) As Double
        Dim distancia As Double
        distancia = Math.Sqrt((a(0) - b(0)) ^ 2 + (a(1) - b(1)) ^ 2)
        Return distancia
    End Function

End Module
