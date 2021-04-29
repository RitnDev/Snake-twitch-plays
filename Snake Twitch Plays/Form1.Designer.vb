<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Snake
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Snake))
        Me.Panel_Global = New System.Windows.Forms.Panel()
        Me.Panel_Status_Nourriture = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Panel_Bordure = New System.Windows.Forms.Panel()
        Me.LB_Status = New System.Windows.Forms.Label()
        Me.Panel_InGame = New System.Windows.Forms.Panel()
        Me.LB_Flux = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Panel_LeaderBoard = New System.Windows.Forms.Panel()
        Me.List_Points = New System.Windows.Forms.ListBox()
        Me.List_Joueurs = New System.Windows.Forms.ListBox()
        Me.LB_Titre_Lead = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.LB_Quitter = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LB_by_START = New System.Windows.Forms.Label()
        Me.Actualisation = New System.Windows.Forms.Timer(Me.components)
        Me.LB_Version = New System.Windows.Forms.Label()
        Me.Timer_Status = New System.Windows.Forms.Timer(Me.components)
        Me.TimerNourriture = New System.Windows.Forms.Timer(Me.components)
        Me.Actualisation_Nourriture = New System.Windows.Forms.Timer(Me.components)
        Me.Panel_Global.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel_Bordure.SuspendLayout()
        Me.Panel_InGame.SuspendLayout()
        Me.Panel_LeaderBoard.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel_Global
        '
        Me.Panel_Global.Controls.Add(Me.Panel_Status_Nourriture)
        Me.Panel_Global.Controls.Add(Me.PictureBox1)
        Me.Panel_Global.Controls.Add(Me.Panel_Bordure)
        Me.Panel_Global.Controls.Add(Me.Panel_InGame)
        Me.Panel_Global.Controls.Add(Me.Panel_LeaderBoard)
        Me.Panel_Global.Controls.Add(Me.Button1)
        resources.ApplyResources(Me.Panel_Global, "Panel_Global")
        Me.Panel_Global.Name = "Panel_Global"
        '
        'Panel_Status_Nourriture
        '
        resources.ApplyResources(Me.Panel_Status_Nourriture, "Panel_Status_Nourriture")
        Me.Panel_Status_Nourriture.Name = "Panel_Status_Nourriture"
        '
        'PictureBox1
        '
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'Panel_Bordure
        '
        Me.Panel_Bordure.BackColor = System.Drawing.Color.SlateBlue
        Me.Panel_Bordure.Controls.Add(Me.LB_Status)
        resources.ApplyResources(Me.Panel_Bordure, "Panel_Bordure")
        Me.Panel_Bordure.Name = "Panel_Bordure"
        '
        'LB_Status
        '
        resources.ApplyResources(Me.LB_Status, "LB_Status")
        Me.LB_Status.ForeColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(13, Byte), Integer))
        Me.LB_Status.Name = "LB_Status"
        '
        'Panel_InGame
        '
        Me.Panel_InGame.BackColor = System.Drawing.Color.FromArgb(CType(CType(5, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.Panel_InGame.Controls.Add(Me.LB_Flux)
        Me.Panel_InGame.Controls.Add(Me.Label14)
        resources.ApplyResources(Me.Panel_InGame, "Panel_InGame")
        Me.Panel_InGame.Name = "Panel_InGame"
        '
        'LB_Flux
        '
        resources.ApplyResources(Me.LB_Flux, "LB_Flux")
        Me.LB_Flux.Name = "LB_Flux"
        '
        'Label14
        '
        resources.ApplyResources(Me.Label14, "Label14")
        Me.Label14.Name = "Label14"
        '
        'Panel_LeaderBoard
        '
        Me.Panel_LeaderBoard.Controls.Add(Me.List_Points)
        Me.Panel_LeaderBoard.Controls.Add(Me.List_Joueurs)
        Me.Panel_LeaderBoard.Controls.Add(Me.LB_Titre_Lead)
        resources.ApplyResources(Me.Panel_LeaderBoard, "Panel_LeaderBoard")
        Me.Panel_LeaderBoard.Name = "Panel_LeaderBoard"
        '
        'List_Points
        '
        Me.List_Points.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(13, Byte), Integer))
        Me.List_Points.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        resources.ApplyResources(Me.List_Points, "List_Points")
        Me.List_Points.ForeColor = System.Drawing.Color.SlateBlue
        Me.List_Points.FormattingEnabled = True
        Me.List_Points.Name = "List_Points"
        '
        'List_Joueurs
        '
        Me.List_Joueurs.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(13, Byte), Integer))
        Me.List_Joueurs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.List_Joueurs.CausesValidation = False
        resources.ApplyResources(Me.List_Joueurs, "List_Joueurs")
        Me.List_Joueurs.ForeColor = System.Drawing.Color.SlateBlue
        Me.List_Joueurs.FormattingEnabled = True
        Me.List_Joueurs.Name = "List_Joueurs"
        '
        'LB_Titre_Lead
        '
        resources.ApplyResources(Me.LB_Titre_Lead, "LB_Titre_Lead")
        Me.LB_Titre_Lead.Name = "LB_Titre_Lead"
        '
        'Button1
        '
        resources.ApplyResources(Me.Button1, "Button1")
        Me.Button1.Name = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'LB_Quitter
        '
        resources.ApplyResources(Me.LB_Quitter, "LB_Quitter")
        Me.LB_Quitter.Name = "LB_Quitter"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'LB_by_START
        '
        resources.ApplyResources(Me.LB_by_START, "LB_by_START")
        Me.LB_by_START.Name = "LB_by_START"
        '
        'Actualisation
        '
        Me.Actualisation.Interval = 500
        '
        'LB_Version
        '
        resources.ApplyResources(Me.LB_Version, "LB_Version")
        Me.LB_Version.Name = "LB_Version"
        '
        'Timer_Status
        '
        Me.Timer_Status.Interval = 10000
        '
        'TimerNourriture
        '
        Me.TimerNourriture.Interval = 30000
        '
        'Actualisation_Nourriture
        '
        Me.Actualisation_Nourriture.Interval = 500
        '
        'Snake
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(4, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(13, Byte), Integer))
        Me.ControlBox = False
        Me.Controls.Add(Me.LB_Version)
        Me.Controls.Add(Me.Panel_Global)
        Me.Controls.Add(Me.LB_by_START)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LB_Quitter)
        Me.ForeColor = System.Drawing.Color.SlateBlue
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Snake"
        Me.Panel_Global.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel_Bordure.ResumeLayout(False)
        Me.Panel_InGame.ResumeLayout(False)
        Me.Panel_InGame.PerformLayout()
        Me.Panel_LeaderBoard.ResumeLayout(False)
        Me.Panel_LeaderBoard.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel_Global As Panel
    Friend WithEvents LB_Quitter As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents LB_by_START As Label
    Friend WithEvents Panel_InGame As Panel
    Friend WithEvents LB_Flux As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents Actualisation As Timer
    Friend WithEvents Panel_Bordure As Panel
    Friend WithEvents Panel_LeaderBoard As Panel
    Friend WithEvents List_Points As ListBox
    Friend WithEvents List_Joueurs As ListBox
    Friend WithEvents LB_Titre_Lead As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents LB_Version As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents LB_Status As Label
    Friend WithEvents Timer_Status As Timer
    Friend WithEvents Panel_Status_Nourriture As Panel
    Friend WithEvents TimerNourriture As Timer
    Friend WithEvents Actualisation_Nourriture As Timer
End Class
