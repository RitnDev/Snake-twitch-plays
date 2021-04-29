Public Class Options

    'Bouton COMMENCER LA PARTIE !
    Private Sub BP_Start_Click(sender As Object, e As EventArgs) Handles BP_Start.Click

        'Verification de la saisie du champs TB_Score (seulement des chiffres)
        If TB_Score.Text <> Nothing Then
            If TB_Score.Text <> 0 Then

                For i = 1 To Len(TB_Score.Text)
                    Select Case Mid(TB_Score.Text, i, 1)
                        Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
                            'ne rien faire
                        Case Else
                            'si jamais il y a un connard qui a mis autres choses qu'un chiffre tu te prends ce msg dans la gueule :
                            MsgBox("Votre saisie est incorrect !", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Score Incorrect !")
                            Exit Sub
                    End Select
                Next
                'Si la saisie est correct on l'inscrit dans la variable Options_Jeu
                Snake.Options_Jeu.Nb_Points = TB_Score.Text

                Snake.Options_Jeu.Plateau_horizontale = 27 'Nombre de Ligne
                Snake.Options_Jeu.Plateau_Verticale = 30   'Nombre de Colonne

                Snake.Options_Jeu.Temps_Nourriture = 5 'temps en seconde (30sec)

                'On ferme la petite fenêtre d'option
                Me.Close()
                'On lance la game !!!
                Snake.NouvellePartie()

            End If
        End If

    End Sub

    'RadioButton Souris
    Private Sub Radio_Souris_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_Souris.CheckedChanged
        If sender.checked = True Then
            Snake.Options_Jeu.Nourriture = Enum_Nourriture.Souris
            Snake.Panel_Status_Nourriture.BackgroundImage = Global.Snake_Twitch_Plays.My.Resources.SOURIS
        End If
    End Sub

    'RadioButton Pomme
    Private Sub Radio_Pomme_CheckedChanged(sender As Object, e As EventArgs) Handles Radio_Pomme.CheckedChanged
        If sender.checked = True Then
            Snake.Options_Jeu.Nourriture = Enum_Nourriture.Pomme
            Snake.Panel_Status_Nourriture.BackgroundImage = Global.Snake_Twitch_Plays.My.Resources.POMME
        End If
    End Sub

    'CheckBox Restart
    Private Sub Check_restart_CheckedChanged(sender As Object, e As EventArgs) Handles Check_restart.CheckedChanged
        Snake.Options_Jeu.Restart = sender.checked
    End Sub
End Class