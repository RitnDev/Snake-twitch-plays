<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Options
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Options))
        Me.Radio_Souris = New System.Windows.Forms.RadioButton()
        Me.Radio_Pomme = New System.Windows.Forms.RadioButton()
        Me.GrpB_Nourriture = New System.Windows.Forms.GroupBox()
        Me.GrpB_Param = New System.Windows.Forms.GroupBox()
        Me.Check_restart = New System.Windows.Forms.CheckBox()
        Me.LB_PointsNour = New System.Windows.Forms.Label()
        Me.TB_Score = New System.Windows.Forms.TextBox()
        Me.BP_Start = New System.Windows.Forms.Button()
        Me.GrpB_Nourriture.SuspendLayout()
        Me.GrpB_Param.SuspendLayout()
        Me.SuspendLayout()
        '
        'Radio_Souris
        '
        Me.Radio_Souris.AutoSize = True
        Me.Radio_Souris.Checked = True
        Me.Radio_Souris.Location = New System.Drawing.Point(18, 26)
        Me.Radio_Souris.Name = "Radio_Souris"
        Me.Radio_Souris.Size = New System.Drawing.Size(66, 23)
        Me.Radio_Souris.TabIndex = 0
        Me.Radio_Souris.TabStop = True
        Me.Radio_Souris.Text = "Souris"
        Me.Radio_Souris.UseVisualStyleBackColor = True
        '
        'Radio_Pomme
        '
        Me.Radio_Pomme.AutoSize = True
        Me.Radio_Pomme.Location = New System.Drawing.Point(18, 55)
        Me.Radio_Pomme.Name = "Radio_Pomme"
        Me.Radio_Pomme.Size = New System.Drawing.Size(75, 23)
        Me.Radio_Pomme.TabIndex = 1
        Me.Radio_Pomme.Text = "Pomme"
        Me.Radio_Pomme.UseVisualStyleBackColor = True
        '
        'GrpB_Nourriture
        '
        Me.GrpB_Nourriture.Controls.Add(Me.Radio_Souris)
        Me.GrpB_Nourriture.Controls.Add(Me.Radio_Pomme)
        Me.GrpB_Nourriture.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpB_Nourriture.Location = New System.Drawing.Point(12, 12)
        Me.GrpB_Nourriture.Name = "GrpB_Nourriture"
        Me.GrpB_Nourriture.Size = New System.Drawing.Size(304, 94)
        Me.GrpB_Nourriture.TabIndex = 2
        Me.GrpB_Nourriture.TabStop = False
        Me.GrpB_Nourriture.Text = "Nourriture"
        '
        'GrpB_Param
        '
        Me.GrpB_Param.Controls.Add(Me.Check_restart)
        Me.GrpB_Param.Controls.Add(Me.LB_PointsNour)
        Me.GrpB_Param.Controls.Add(Me.TB_Score)
        Me.GrpB_Param.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpB_Param.Location = New System.Drawing.Point(12, 112)
        Me.GrpB_Param.Name = "GrpB_Param"
        Me.GrpB_Param.Size = New System.Drawing.Size(304, 112)
        Me.GrpB_Param.TabIndex = 3
        Me.GrpB_Param.TabStop = False
        Me.GrpB_Param.Text = "Paramètres du jeu"
        '
        'Check_restart
        '
        Me.Check_restart.AutoSize = True
        Me.Check_restart.Checked = True
        Me.Check_restart.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Check_restart.Location = New System.Drawing.Point(18, 78)
        Me.Check_restart.Name = "Check_restart"
        Me.Check_restart.Size = New System.Drawing.Size(109, 23)
        Me.Check_restart.TabIndex = 2
        Me.Check_restart.Text = "Restart Auto"
        Me.Check_restart.UseVisualStyleBackColor = True
        '
        'LB_PointsNour
        '
        Me.LB_PointsNour.AutoSize = True
        Me.LB_PointsNour.Location = New System.Drawing.Point(14, 43)
        Me.LB_PointsNour.Name = "LB_PointsNour"
        Me.LB_PointsNour.Size = New System.Drawing.Size(117, 19)
        Me.LB_PointsNour.TabIndex = 1
        Me.LB_PointsNour.Text = "Pts / Nourriture :"
        '
        'TB_Score
        '
        Me.TB_Score.Location = New System.Drawing.Point(137, 40)
        Me.TB_Score.MaxLength = 4
        Me.TB_Score.Name = "TB_Score"
        Me.TB_Score.Size = New System.Drawing.Size(61, 27)
        Me.TB_Score.TabIndex = 0
        Me.TB_Score.Text = "10"
        '
        'BP_Start
        '
        Me.BP_Start.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BP_Start.Location = New System.Drawing.Point(12, 230)
        Me.BP_Start.Name = "BP_Start"
        Me.BP_Start.Size = New System.Drawing.Size(304, 34)
        Me.BP_Start.TabIndex = 4
        Me.BP_Start.Text = "Commencer la partie"
        Me.BP_Start.UseVisualStyleBackColor = True
        '
        'Options
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(328, 276)
        Me.Controls.Add(Me.BP_Start)
        Me.Controls.Add(Me.GrpB_Param)
        Me.Controls.Add(Me.GrpB_Nourriture)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Options"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Options"
        Me.GrpB_Nourriture.ResumeLayout(False)
        Me.GrpB_Nourriture.PerformLayout()
        Me.GrpB_Param.ResumeLayout(False)
        Me.GrpB_Param.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Radio_Souris As RadioButton
    Friend WithEvents Radio_Pomme As RadioButton
    Friend WithEvents GrpB_Nourriture As GroupBox
    Friend WithEvents GrpB_Param As GroupBox
    Friend WithEvents Check_restart As CheckBox
    Friend WithEvents LB_PointsNour As Label
    Friend WithEvents TB_Score As TextBox
    Friend WithEvents BP_Start As Button
End Class
