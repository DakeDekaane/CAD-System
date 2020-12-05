Public Class Main
    Private Sub ConexiónAAutoCADToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConexiónAAutoCADToolStripMenuItem.Click
        'Conexión a AutoCAD
        InicializaConexion()
        If Not document Is Nothing Then
            dwgActual.Text = "Plano conectado: " & document.Name
        Else
            dwgActual.Text = "No estamos conectados aún"
        End If
    End Sub
End Class
