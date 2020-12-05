Module Conexion
    Public document As AutoCAD.AcadDocument         'Plano activo AutoCAD
    Public AutoCADApp As AutoCAD.AcadApplication    'Aplicación AutoCAD
    Public utility As AutoCAD.AcadUtility           'Utilerías AutoCAD

    'Public Sub InicializaConexion(ByVal version As String)
    Public Sub InicializaConexion()
        'Revisa la existencia de un proceso de la clase r
        'Si existe, proporciona la conexión al documento activo (plano)
        'Deja visible AutoCAD
        'Si no hay proceso activo, se envía un mensaje

        Dim r As String = "AUTOCAD.Application"

        Try
            document = Nothing
            AutoCADApp = GetObject(, r)             'Acceso al proceso de AutoCAD activo
            document = AutoCADApp.ActiveDocument    'Acceso al plano activo
            utility = document.Utility              'Acceso al objeto utilerías de AutoCAD
            AutoCADApp.Visible = True               'Se hace visible el proceso

        Catch ex As Exception
            'Se muestra el mensaje del error en caso de acceso fallido
            MsgBox(ex.Message, MsgBoxStyle.Information, "CAD")

        End Try
    End Sub
End Module
