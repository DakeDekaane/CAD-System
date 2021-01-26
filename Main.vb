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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Me.Visible = False
        AgregarAuto()
        'Me.Visible = True
    End Sub

    Private Sub VialidadToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VialidadToolStripMenuItem.Click
        'Me.Visible = False
        ConfigurarTipo("Vialidad")
        'Me.Visible = True
    End Sub

    Private Sub TuberíaDeAguaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TuberíaDeAguaToolStripMenuItem.Click
        'Me.Visible = False
        ConfigurarTipo("Tuberia de Agua")
        'Me.Visible = True
    End Sub

    Private Sub PredioToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PredioToolStripMenuItem1.Click
        ConfigurarTipo("Predio")
    End Sub

    Private Sub DueñoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DueñoToolStripMenuItem.Click
        ConfigurarPropiedad("Dueño", "Indique el nombre del dueño")
    End Sub

    Private Sub HabitantesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HabitantesToolStripMenuItem.Click
        ConfigurarPropiedad("Habitantes", "Indique el número de habitantes")
    End Sub

    Private Sub NúmeroDePisosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NúmeroDePisosToolStripMenuItem.Click
        ConfigurarPropiedad("Pisos", "Indique el número de pisos")
    End Sub

    Private Sub ÁreaDeDesplanteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ÁreaDeDesplanteToolStripMenuItem.Click
        ConfigurarPropiedad("A.Desplante", "Indique el área de desplante")
    End Sub

    Private Sub ÁreaDeConstruccionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ÁreaDeConstruccionToolStripMenuItem.Click
        ConfigurarPropiedad("A.Construcción", "Indique el área de construcción")
    End Sub

    Private Sub TipoDeElementoToolStripMenuItem_Click(sender As Object, e As EventArgs)
        'Me.Visible = False
        ObtenerPropiedad("Tipo")
        'Me.Visible = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MostrarInformacionPredio()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ConectarPredioAguaPotable()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim tiempo As Integer = CInt(TextBoxTiempo.Text)
        If tiempo > 0 Then
            For i = 1 To tiempo
                Simular()
            Next
        End If

        'Catch ex As Exception
        'MsgBox(ex.Message, MsgBoxStyle.Information)
        'End Try

    End Sub

    Private Sub SemaforoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SemaforoToolStripMenuItem.Click
        ConfigurarTipo("Semaforo")
    End Sub

    Private Sub MostrarPropiedadesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MostrarPropiedadesToolStripMenuItem.Click
        ObtenerPropiedad("Tipo")
    End Sub

    Private Sub VerdeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerdeToolStripMenuItem.Click
        ReiniciarSemaforo("Verde")
    End Sub

    Private Sub RojoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RojoToolStripMenuItem.Click
        ReiniciarSemaforo("Rojo")

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ColocarSentido()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        CambiarSentido()
    End Sub
End Class
