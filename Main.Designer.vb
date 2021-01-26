<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ConexiónAAutoCADToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AgregarPropiedadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VialidadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TuberíaDeAguaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PredioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PredioToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DueñoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HabitantesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NúmeroDePisosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ÁreaDeDesplanteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ÁreaDeConstruccionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SemaforoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MostrarPropiedadesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReiniciarSemáforoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VerdeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RojoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.dwgActual = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ListBoxPredios = New System.Windows.Forms.ListBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBoxTiempo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConexiónAAutoCADToolStripMenuItem, Me.AgregarPropiedadToolStripMenuItem, Me.MostrarPropiedadesToolStripMenuItem, Me.ReiniciarSemáforoToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(514, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ConexiónAAutoCADToolStripMenuItem
        '
        Me.ConexiónAAutoCADToolStripMenuItem.Name = "ConexiónAAutoCADToolStripMenuItem"
        Me.ConexiónAAutoCADToolStripMenuItem.Size = New System.Drawing.Size(132, 20)
        Me.ConexiónAAutoCADToolStripMenuItem.Text = "Conexión a AutoCAD"
        '
        'AgregarPropiedadToolStripMenuItem
        '
        Me.AgregarPropiedadToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VialidadToolStripMenuItem, Me.TuberíaDeAguaToolStripMenuItem, Me.PredioToolStripMenuItem, Me.SemaforoToolStripMenuItem})
        Me.AgregarPropiedadToolStripMenuItem.Name = "AgregarPropiedadToolStripMenuItem"
        Me.AgregarPropiedadToolStripMenuItem.Size = New System.Drawing.Size(129, 20)
        Me.AgregarPropiedadToolStripMenuItem.Text = "Agregar información"
        '
        'VialidadToolStripMenuItem
        '
        Me.VialidadToolStripMenuItem.Name = "VialidadToolStripMenuItem"
        Me.VialidadToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.VialidadToolStripMenuItem.Text = "Vialidad"
        '
        'TuberíaDeAguaToolStripMenuItem
        '
        Me.TuberíaDeAguaToolStripMenuItem.Name = "TuberíaDeAguaToolStripMenuItem"
        Me.TuberíaDeAguaToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.TuberíaDeAguaToolStripMenuItem.Text = "Tubería de agua"
        '
        'PredioToolStripMenuItem
        '
        Me.PredioToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PredioToolStripMenuItem1, Me.DueñoToolStripMenuItem, Me.HabitantesToolStripMenuItem, Me.NúmeroDePisosToolStripMenuItem, Me.ÁreaDeDesplanteToolStripMenuItem, Me.ÁreaDeConstruccionToolStripMenuItem})
        Me.PredioToolStripMenuItem.Name = "PredioToolStripMenuItem"
        Me.PredioToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.PredioToolStripMenuItem.Text = "Predio"
        '
        'PredioToolStripMenuItem1
        '
        Me.PredioToolStripMenuItem1.Name = "PredioToolStripMenuItem1"
        Me.PredioToolStripMenuItem1.Size = New System.Drawing.Size(186, 22)
        Me.PredioToolStripMenuItem1.Text = "Predio"
        '
        'DueñoToolStripMenuItem
        '
        Me.DueñoToolStripMenuItem.Name = "DueñoToolStripMenuItem"
        Me.DueñoToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.DueñoToolStripMenuItem.Text = "Dueño"
        '
        'HabitantesToolStripMenuItem
        '
        Me.HabitantesToolStripMenuItem.Name = "HabitantesToolStripMenuItem"
        Me.HabitantesToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.HabitantesToolStripMenuItem.Text = "Habitantes"
        '
        'NúmeroDePisosToolStripMenuItem
        '
        Me.NúmeroDePisosToolStripMenuItem.Name = "NúmeroDePisosToolStripMenuItem"
        Me.NúmeroDePisosToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.NúmeroDePisosToolStripMenuItem.Text = "Número de pisos"
        '
        'ÁreaDeDesplanteToolStripMenuItem
        '
        Me.ÁreaDeDesplanteToolStripMenuItem.Name = "ÁreaDeDesplanteToolStripMenuItem"
        Me.ÁreaDeDesplanteToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.ÁreaDeDesplanteToolStripMenuItem.Text = "Área de desplante"
        '
        'ÁreaDeConstruccionToolStripMenuItem
        '
        Me.ÁreaDeConstruccionToolStripMenuItem.Name = "ÁreaDeConstruccionToolStripMenuItem"
        Me.ÁreaDeConstruccionToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.ÁreaDeConstruccionToolStripMenuItem.Text = "Área de construcción"
        '
        'SemaforoToolStripMenuItem
        '
        Me.SemaforoToolStripMenuItem.Name = "SemaforoToolStripMenuItem"
        Me.SemaforoToolStripMenuItem.Size = New System.Drawing.Size(158, 22)
        Me.SemaforoToolStripMenuItem.Text = "Semaforo"
        '
        'MostrarPropiedadesToolStripMenuItem
        '
        Me.MostrarPropiedadesToolStripMenuItem.Name = "MostrarPropiedadesToolStripMenuItem"
        Me.MostrarPropiedadesToolStripMenuItem.Size = New System.Drawing.Size(128, 20)
        Me.MostrarPropiedadesToolStripMenuItem.Text = "Mostrar propiedades"
        '
        'ReiniciarSemáforoToolStripMenuItem
        '
        Me.ReiniciarSemáforoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.VerdeToolStripMenuItem, Me.RojoToolStripMenuItem})
        Me.ReiniciarSemáforoToolStripMenuItem.Name = "ReiniciarSemáforoToolStripMenuItem"
        Me.ReiniciarSemáforoToolStripMenuItem.Size = New System.Drawing.Size(117, 20)
        Me.ReiniciarSemáforoToolStripMenuItem.Text = "Reiniciar semáforo"
        '
        'VerdeToolStripMenuItem
        '
        Me.VerdeToolStripMenuItem.Name = "VerdeToolStripMenuItem"
        Me.VerdeToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.VerdeToolStripMenuItem.Text = "Verde"
        '
        'RojoToolStripMenuItem
        '
        Me.RojoToolStripMenuItem.Name = "RojoToolStripMenuItem"
        Me.RojoToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.RojoToolStripMenuItem.Text = "Rojo"
        '
        'dwgActual
        '
        Me.dwgActual.AutoSize = True
        Me.dwgActual.Location = New System.Drawing.Point(12, 24)
        Me.dwgActual.Name = "dwgActual"
        Me.dwgActual.Size = New System.Drawing.Size(163, 13)
        Me.dwgActual.TabIndex = 1
        Me.dwgActual.Text = "Se requiere conexión a AutoCAD"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(15, 104)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(180, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Mostrar información"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Predios"
        '
        'ListBoxPredios
        '
        Me.ListBoxPredios.FormattingEnabled = True
        Me.ListBoxPredios.Location = New System.Drawing.Point(15, 142)
        Me.ListBoxPredios.Name = "ListBoxPredios"
        Me.ListBoxPredios.Size = New System.Drawing.Size(180, 134)
        Me.ListBoxPredios.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(268, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Simulación"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(271, 72)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(180, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Colocar automovil"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(271, 192)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(180, 46)
        Me.Button4.TabIndex = 2
        Me.Button4.Text = "Iniciar simulación"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(268, 164)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Tiempo de simulación:"
        '
        'TextBoxTiempo
        '
        Me.TextBoxTiempo.Location = New System.Drawing.Point(386, 160)
        Me.TextBoxTiempo.Name = "TextBoxTiempo"
        Me.TextBoxTiempo.Size = New System.Drawing.Size(32, 20)
        Me.TextBoxTiempo.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(424, 164)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "pasos"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(271, 128)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(85, 23)
        Me.Button5.TabIndex = 2
        Me.Button5.Text = "Colocar"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(15, 72)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(180, 23)
        Me.Button6.TabIndex = 2
        Me.Button6.Text = "Conectar a red de agua potable"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(268, 104)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(108, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Sentido de vialidades"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(367, 128)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(85, 23)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Cambiar"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(514, 303)
        Me.Controls.Add(Me.TextBoxTiempo)
        Me.Controls.Add(Me.ListBoxPredios)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.dwgActual)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Main"
        Me.Text = "Proyecto"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents ConexiónAAutoCADToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents dwgActual As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents ListBoxPredios As ListBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBoxTiempo As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents AgregarPropiedadToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents VialidadToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TuberíaDeAguaToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PredioToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DueñoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HabitantesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NúmeroDePisosToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ÁreaDeDesplanteToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ÁreaDeConstruccionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents PredioToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents MostrarPropiedadesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SemaforoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ReiniciarSemáforoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents VerdeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RojoToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label5 As Label
    Friend WithEvents Button3 As Button
End Class
