Module Consultas
    Public Sub ConfigurarTipo(tipo As String)
        'Agregar un diccionario a un objeto
        'Un diccionario es una estructura de datos similar a una tabla de una base de datos
        Dim entidad As AcadEntity
        AppActivateAutoCAD()
        entidad = ObtenerEntidad("Selecciona un objeto")
        If Not IsNothing(entidad) Then
            AgregarXData(entidad, "Tipo", tipo)
        End If
    End Sub
    Public Sub ConfigurarPropiedad(propiedad As String, mensaje As String)
        'Agregar un diccionario a un objeto
        'Un diccionario es una estructura de datos similar a una tabla de una base de datos
        Dim entidad As AcadEntity
        Dim valor As String
        AppActivateAutoCAD()
        entidad = ObtenerEntidad("Selecciona un objeto")
        If Not IsNothing(entidad) Then
            valor = utility.GetString(1, mensaje)
            If Not IsNothing(valor) Then
                AgregarXData(entidad, propiedad, valor)
            End If
        End If
    End Sub
    Public Sub ConfigurarPropiedad(entidad As AcadEntity, propiedad As String, valor As String)
        'Agregar un diccionario a un objeto
        'Un diccionario es una estructura de datos similar a una tabla de una base de datos
        If Not IsNothing(entidad) Then
            AgregarXData(entidad, propiedad, valor)
        End If
    End Sub
    Public Sub AgregarXData(entidad As AcadEntity, nameXRecord As String, valor As String)
        'Agrega un XRecord y su valor a la entidad
        Dim dictAsti As AcadDictionary
        Dim astiXRec As AcadXRecord
        Dim keyCode() As Short 'Obligatorio que sea Short
        Dim cptData() As Object 'Obligatorio que sea Object

        ReDim keyCode(0)
        ReDim cptData(0)

        Dim bandaid As Boolean = False
        While Not bandaid
            Try
                dictAsti = entidad.GetExtensionDictionary
                bandaid = True
            Catch ex As System.Runtime.InteropServices.COMException
                bandaid = False
            End Try
        End While
        '---
        astiXRec = dictAsti.AddXRecord(nameXRecord.ToUpper.Trim)
        keyCode(0) = 100 : cptData(0) = valor
        astiXRec.SetXRecordData(keyCode, cptData)
    End Sub

    Public Function ObtenerPropiedad(propiedad As String) As String
        Dim entity As AcadEntity
        Dim tipo As String = Nothing

        AppActivateAutoCAD()
        entity = ObtenerEntidad("Selecciona un objeto")

        If Not IsNothing(entity) Then
            'recuperando XRecord material y se extrae su unico valor
            ObtenerXData(entity, propiedad, tipo)
            If tipo Is Nothing Then
                MsgBox("Objeto sin tipo", MsgBoxStyle.Information)
                'utility.Prompt("Objeto sin " & propiedad & " asignado")
            Else
                MsgBox(tipo & " seleccionado", MsgBoxStyle.Information)
                'utility.Prompt(propiedad & ": " & tipo)
            End If
        End If
            Return tipo
    End Function
    Public Function ObtenerPropiedad(entidad As AcadEntity, propiedad As String) As String
        Dim tipo As String = Nothing

        If Not IsNothing(entidad) Then
            'recuperando XRecord material y se extrae su unico valor
            ObtenerXData(entidad, propiedad, tipo)
            'If tipo Is Nothing Then
            'utility.Prompt("Objeto sin " & propiedad & " asignado")
            'Else
            'tipo = tipo.Trim
            'utility.Prompt("Tipo: " & tipo)
            'End If
        End If

        Return tipo
    End Function

    Public Sub ObtenerXData(entity As AcadEntity, nameXRecord As String, ByRef valor As String)
        Dim astiXRec As AcadXRecord = Nothing
        Dim dictAsti As AcadDictionary
        Dim getKey As Object = Nothing
        Dim getData As Object = Nothing

        valor = Nothing

        'Accede al diccionario de la entidad
        '---
        Dim bandaid As Boolean = False
        While Not bandaid
            Try
                dictAsti = entity.GetExtensionDictionary '!!!'
                bandaid = True
            Catch ex As System.Runtime.InteropServices.COMException
                bandaid = False
            End Try
        End While
        '---

        Try
            ' Accediendo al XRecor solicitado por el nombre
            astiXRec = dictAsti.Item(nameXRecord.ToUpper.Trim)
        Catch ex As Exception
            'no existe el XRecord solicitado
        End Try
        If Not IsNothing(astiXRec) Then
            'Accedo a las columnas del XRecord
            '---
            bandaid = False
            While Not bandaid
                Try
                    astiXRec.GetXRecordData(getKey, getData) '!!!'
                    bandaid = True
                Catch ex As System.Runtime.InteropServices.COMException
                    bandaid = False
                End Try
            End While
            '---
            'Si hay columnas, se accede al valor de la primera columna
            If Not IsNothing(getData) Then
                valor = getData(0)
            End If
        End If
    End Sub
    Public Sub MostrarInformacionPredio()
        Dim predio As AcadEntity = Nothing
        Dim p(0 To 2) As Double
        Dim listbox As ListBox = Main.ListBoxPredios

        listbox.Items.Clear()
        AppActivateAutoCAD()

        'Try
        utility.GetEntity(predio, p, "Selecciona un predio")
            If ObtenerPropiedad(predio, "Tipo") = "Predio" Then
                listbox.Items.Add("Dueño: " & ObtenerPropiedad(predio, "Dueño"))
                listbox.Items.Add("Habitantes: " & ObtenerPropiedad(predio, "Habitantes"))
                listbox.Items.Add("Pisos: " & ObtenerPropiedad(predio, "Pisos"))
                listbox.Items.Add("A.Desplante: " & ObtenerPropiedad(predio, "A.Desplante"))
                listbox.Items.Add("A.Construcción: " & ObtenerPropiedad(predio, "A.Construcción"))
                listbox.Items.Add("Conexión agua potable:" & ObtenerPropiedad(predio, "AguaPotable"))
            End If
        'Catch ex As Exception
        'MsgBox(ex.Message, MsgBoxStyle.Information, "CAD")
        'End Try
    End Sub

    Public Sub ConectarPredioAguaPotable()
        Dim predio As Acad3DSolid
        Dim p() As Double           'Centroide de predio'
        Dim p1(0 To 2) As Double          'Punto auxiliar'
        Dim p2(0 To 2) As Double          'Punto auxiliar'
        Dim corners() As Double           'Arreglo para almacenar los puntos del polígono de selección
        Dim objectSet As AcadSelectionSet 'Conjunto para almacenar los objetos encontrados
        Dim radio As Double              'Radio del círculo
        Dim d As Double
        Dim a As Double
        Dim linea As AcadLine

        'Dim perimeter As AcadPolyline    'Polyline para el rectángulo

        'Se enfoca AutoCAD
        AppActivateAutoCAD()
        'Se lee una coordenada
        predio = ObtenerEntidad("Seleccione el predio")

        Dim success = False
        While Not success
            Try
                If Not IsNothing(predio) Then
                    p = predio.Centroid     'Se obtiene el centroide del predio
                    p(2) = 0                'Se hace z= 0

                    predio.GetBoundingBox(p1, p2)
                    p1(2) = 0
                    radio = Dist2Points(p, p1)

                    If Not IsNothing(radio) Then
                        'Se determinan puntos del circulo
                        corners = GeneraCoordenadasCirculo(p, radio, 0, 360, 20)

                        'Se crea el conjunto vacío
                        objectSet = CrearConjuntoVacio(document, "IDLE")

                        If Not IsNothing(objectSet) Then
                            'Se añaden los objetos dentro del rectángulo al conjunto
                            objectSet.SelectByPolygon(AcSelect.acSelectionSetCrossingPolygon, corners)
                            For Each element In objectSet
                                If ObtenerPropiedad(element, "Tipo") = "Tuberia de Agua" Then
                                    d = Dist2Line(p, element, p1)
                                    linea = document.ModelSpace.AddLine(p, p1)
                                    linea.Update()
                                    ConfigurarPropiedad(predio, "AguaPotable", linea.Handle)
                                    ConfigurarPropiedad(linea, "Tipo", "Conexión Agua")
                                    Exit For
                                End If
                            Next
                            objectSet.Delete()
                        End If
                    End If
                End If
                success = True
            Catch ex As Exception
                success = False
            End Try
        End While
    End Sub

End Module
