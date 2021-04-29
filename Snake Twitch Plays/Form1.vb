Imports System.IO
Imports ClassMD
Imports ClassMD.GestionFichier.FichierTXT
Imports ClassMD.GestionDossier.Dossier
Imports ClassMD.ControlForm


Public Structure Struct_Directions
    Public Haut As Boolean
    Public Bas As Boolean
    Public Gauche As Boolean
    Public Droite As Boolean
    Enum Sens As Byte
        Aucun = 0
        Haut = 1
        Bas = 2
        Gauche = 3
        Droite = 4
    End Enum
End Structure

Public Structure Struct_Position
    Public emplacement As Integer   'Numero de case du plateau
    '0 = en haut à gauche
    Public horizontale As Integer   'Coordonnée horizontale
    Public verticale As Integer     'Coordonné verticale
End Structure


Structure Struct_Serpent_Tête
    Public position As Struct_Position
End Structure

Structure Struct_Serpent_Corp
    Public position As Struct_Position
    Public plein As Integer
End Structure

Structure Struct_Serpent
    Public Taille As Integer
    Public Tête As Struct_Serpent_Tête
    Public Corps() As Struct_Serpent_Corp
    Public Coups As Integer

    Enum TabSerpent As Byte
        position = 0
        horizontale = 1
        verticale = 2
        plein = 3
    End Enum
End Structure

Structure Struct_Nourriture
    Public position As Struct_Position
    Public Coups As Integer
End Structure

Public Structure Struct_Options_Jeu
    Public Nourriture As Integer
    Public Nb_Points As Integer
    Public Restart As Boolean
    Public Plateau_horizontale As Integer
    Public Plateau_Verticale As Integer
    Public Temps_Nourriture As Integer
End Structure

Public Enum Enum_Nourriture As Integer
    Souris = 0
    Pomme = 1
End Enum

Public Structure Struct_Joueur
    Dim Pseudo As String
    Dim Points As Integer
End Structure





Public Class Snake

    Public Version As String


    Public chemin(0 To 100) As String
    Dim cheminCommandeTXT, cheminOriginal As String
    Private IsFormBeingDragged As Boolean = False   'Utiliser pour le déplacement de la fenêtre
    Private MouseDownX As Integer                   'Utiliser pour le déplacement de la fenêtre
    Private MouseDownY As Integer                   'Utiliser pour le déplacement de la fenêtre


    Dim Plateau As New Collection
    Dim perdu As Boolean = False
    Dim direction As Struct_Directions
    Dim serpent As Struct_Serpent
    Dim Nourriture As Struct_Nourriture
    Public Options_Jeu As Struct_Options_Jeu
    Dim Leaderboard As New List(Of Struct_Joueur)
    Dim Joueur, commande(0 To 5) As String
    Dim direction_Param As New List(Of Struct_Directions)
    Dim join As Boolean




#Region "Fonctions"

    Function Pos_Retrouve(ByVal position As Struct_Position) As Integer
        Pos_Retrouve = ((position.horizontale - 1) * Options_Jeu.Plateau_Verticale) + position.verticale
    End Function

    Function Pos_Horizontale(ByVal Emplacement As Integer) As Integer
        Pos_Horizontale = Int(Emplacement / Options_Jeu.Plateau_Verticale) + 1
        If Emplacement Mod Options_Jeu.Plateau_Verticale = 0 Then
            Pos_Horizontale = Pos_Horizontale - 1
        End If
    End Function

    Function Pos_Verticale(ByVal Emplacement As Integer) As Integer
        Pos_Verticale = Emplacement Mod Options_Jeu.Plateau_Verticale
        If Pos_Verticale = 0 Then
            Pos_Verticale = Options_Jeu.Plateau_Verticale
        End If
    End Function

    Function serpent_colision(Optional ByVal Tête As Boolean = False) As Boolean

        serpent_colision = False

        Dim position As Integer = serpent.Tête.position.emplacement

        'Prend en compte la tête dans la recherche de colision
        If Tête = True Then
            position = Nourriture.position.emplacement

            If position = serpent.Tête.position.emplacement Then
                serpent_colision = True
            End If
        End If

        For i = 0 To 808
            If position = serpent.Corps(i).position.emplacement Then
                serpent_colision = True
            End If
        Next

    End Function

    Function Nourriture_colision(ByVal position_nourriture As Struct_Position) As Boolean

        Nourriture_colision = False

        If position_nourriture.emplacement = serpent.Tête.position.emplacement Then
            Nourriture_colision = True
        End If

        If Nourriture_colision = False Then
            For i = 0 To 808
                If position_nourriture.emplacement = serpent.Corps(i).position.emplacement Then
                    Nourriture_colision = True
                    Exit For
                End If
            Next
        End If

    End Function

    Function Retrouve_Joueur(ByVal Leaderboard As List(Of Struct_Joueur), ByVal Joueur_actuel As Struct_Joueur) As Struct_Joueur
        Retrouve_Joueur = Leaderboard.Find(
            Function(joueur)
                Return joueur.Pseudo = Joueur_actuel.Pseudo
            End Function)
    End Function

    Function Deplacement(ByVal Position_actuel As Struct_Position, ByVal Direction As Struct_Directions) As Struct_Position

        Dim position As Struct_Position = Position_actuel

        '   -------------- Vers le haut
        If Direction.Haut = True Then

            position.horizontale = Position_actuel.horizontale - 1

            '-------------- Vers le bas
        ElseIf Direction.Bas = True Then

            position.horizontale = Position_actuel.horizontale + 1

            '--------------Vers la gauche
        ElseIf Direction.Gauche = True Then

            position.verticale = Position_actuel.verticale - 1

            '-------------- Vers la droite
        ElseIf Direction.Droite = True Then

            position.verticale = Position_actuel.verticale + 1

        End If

        position.emplacement = Pos_Retrouve(position)

        Deplacement = position

    End Function

    'Retourne l'image de la tête
    Private Shared Function GetTETEH() As Bitmap
        Return Global.Snake_Twitch_Plays.My.Resources.TETEH
    End Function
    Private Shared Function GetTETEB() As Bitmap
        Return Global.Snake_Twitch_Plays.My.Resources.TETEB
    End Function
    Private Shared Function GetTETEG() As Bitmap
        Return Global.Snake_Twitch_Plays.My.Resources.TETEG
    End Function
    Private Shared Function GetTETED() As Bitmap
        Return Global.Snake_Twitch_Plays.My.Resources.TETED
    End Function



#End Region



#Region "Routines"

    'NOUVELLE PARTIE DE JEU
    Sub NouvellePartie()

        Direction_Change(Struct_Directions.Sens.Aucun)
        Creation_Plateau()
        Serpent_Initialise()
        Nourriture_Initialise()
        Actualisation.Enabled = True
        Actualisation_Nourriture.Enabled = True
        TimerNourriture.Interval = Options_Jeu.Temps_Nourriture * 1000
        TimerNourriture.Enabled = True
        Panel_Status_Nourriture.Visible = False
        serpent.Coups = 0
        join = False

    End Sub

    'RECOMMENCER
    Sub Recommencer()

        'Fond_Couleur_Initialise()

        Direction_Change(Struct_Directions.Sens.Aucun)
        Serpent_Initialise()
        Nourriture_Initialise()
        Actualisation.Enabled = True
        Actualisation_Nourriture.Enabled = False
        TimerNourriture.Interval = Options_Jeu.Temps_Nourriture * 1000
        TimerNourriture.Enabled = True
        Panel_Status_Nourriture.Visible = False
        serpent.Coups = 0

    End Sub



    Sub Direction_Change(ByVal sens As Byte)

        Select Case sens

            Case Struct_Directions.Sens.Aucun

                direction.Bas = False
                direction.Haut = False
                direction.Gauche = False
                direction.Droite = False

            Case Struct_Directions.Sens.Haut

                If direction.Bas = False Then
                    direction.Haut = True
                    direction.Gauche = False
                    direction.Droite = False
                End If

            Case Struct_Directions.Sens.Bas

                If direction.Haut = False Then
                    direction.Bas = True
                    direction.Gauche = False
                    direction.Droite = False
                End If

            Case Struct_Directions.Sens.Gauche

                If direction.Droite = False Then
                    direction.Gauche = True
                    direction.Haut = False
                    direction.Bas = False
                End If

            Case Struct_Directions.Sens.Droite

                If direction.Gauche = False Then
                    direction.Droite = True
                    direction.Haut = False
                    direction.Bas = False
                End If

            Case Else
                'Rien faire
        End Select


    End Sub

    Sub Config_Direction_Param()

        Dim Param As Struct_Directions
        Param.Bas = False
        Param.Haut = False
        Param.Gauche = False
        Param.Droite = False


        'Aucune Direction
        direction_Param.Add(Param)      'direction_param(0)

        'Direction "Haut"
        Param.Haut = True
        direction_Param.Add(Param)      'direction_param(1)

        'Direction "Bas"
        Param.Haut = False
        Param.Bas = True
        direction_Param.Add(Param)      'direction_param(2)

        'Direction "Gauche"
        Param.Bas = False
        Param.Gauche = True
        direction_Param.Add(Param)      'direction_param(3)

        'Direction "Droite"
        Param.Gauche = False
        Param.Droite = True
        direction_Param.Add(Param)      'direction_param(4)

        'Direction précédente
        Param.Droite = False
        direction_Param.Add(Param)      'direction_param(5)

    End Sub


    Sub Serpent_Initialise()

        For i = 0 To ((Options_Jeu.Plateau_horizontale * Options_Jeu.Plateau_Verticale) - 2)
            serpent.Corps(i).position.emplacement = 0
            serpent.Corps(i).position.horizontale = 0
            serpent.Corps(i).position.verticale = 0
            serpent.Corps(i).plein = 0
        Next

        serpent.Taille = 3

        serpent.Tête.position.emplacement = 375
        serpent.Tête.position.horizontale = Pos_Horizontale(375)
        serpent.Tête.position.verticale = Pos_Verticale(375)

        Plateau.Item(serpent.Tête.position.emplacement).BackgroundImage =
        Global.Snake_Twitch_Plays.My.Resources.TETEH


        serpent.Corps(0).position.emplacement = 405
        serpent.Corps(0).position.horizontale = Pos_Horizontale(405)
        serpent.Corps(0).position.verticale = Pos_Verticale(405)
        serpent.Corps(0).plein = 1

        Plateau.Item(serpent.Corps(0).position.emplacement).BackgroundImage =
        Global.Snake_Twitch_Plays.My.Resources.CORP

        serpent.Corps(1).position.emplacement = 435
        serpent.Corps(1).position.horizontale = Pos_Horizontale(435)
        serpent.Corps(1).position.verticale = Pos_Verticale(435)
        serpent.Corps(1).plein = 1

        Plateau.Item(serpent.Corps(1).position.emplacement).BackgroundImage =
        Global.Snake_Twitch_Plays.My.Resources.CORP

        serpent.Corps(2).position.emplacement = 465
        serpent.Corps(2).position.horizontale = Pos_Horizontale(465)
        serpent.Corps(2).position.verticale = Pos_Verticale(465)
        serpent.Corps(2).plein = 1

        Plateau.Item(serpent.Corps(2).position.emplacement).BackgroundImage =
        Global.Snake_Twitch_Plays.My.Resources.CORP

        For i = 1 To (Options_Jeu.Plateau_horizontale * Options_Jeu.Plateau_Verticale)

            Dim corp As Boolean = False

            For y = 0 To ((Options_Jeu.Plateau_horizontale * Options_Jeu.Plateau_Verticale) - 2)

                If i = serpent.Corps(y).position.emplacement Then
                    corp = True
                End If

            Next

            If i <> Nourriture.position.emplacement Then
                If i <> serpent.Tête.position.emplacement Then
                    If corp = False Then
                        Plateau.Item(i).BackgroundImage = Nothing
                    End If
                End If
            End If

        Next

    End Sub

    Sub Serpent_avance()

        'Recopie l'emplacement du corp dans l'emplacement du corp-1
        For i = 1 To ((Options_Jeu.Plateau_horizontale * Options_Jeu.Plateau_Verticale) - 2)

            Dim y As Integer = ((Options_Jeu.Plateau_horizontale * Options_Jeu.Plateau_Verticale) - 1) - i

            If serpent.Corps(y).plein = 1 Then

                serpent.Corps(y).position.emplacement = serpent.Corps(y - 1).position.emplacement
                serpent.Corps(y).position.horizontale = serpent.Corps(y - 1).position.horizontale
                serpent.Corps(y).position.verticale = serpent.Corps(y - 1).position.verticale

            End If

        Next

        'Corp(0) mis à l'emplacement de la tête
        serpent.Corps(0).position = serpent.Tête.position
        'serpent.Corps(0).position.horizontale = serpent.Tête.position.horizontale
        'serpent.Corps(0).position.verticale = serpent.Tête.position.verticale

        Plateau.Item(serpent.Corps(0).position.emplacement).BackgroundImage =
        Global.Snake_Twitch_Plays.My.Resources.CORP

        'Selon la direction, on gère le nouvel emplacement de la tête
        serpent.Tête.position = Deplacement(serpent.Tête.position, direction)


        '   -------------- Vers le haut
        If direction.Haut = True Then

            'Si le serpent fonce dans le mur du Haut
            If serpent.Tête.position.horizontale = 0 Then
                perdu = True
            End If

            'Affichage de la tête après déplacement
            If perdu = False Then
                Plateau.Item(serpent.Tête.position.emplacement).BackgroundImage = GetTETEH()
            End If

            '-------------- Vers le bas
        ElseIf direction.Bas = True Then

            'Si le serpent se prend le mur du bas
            If serpent.Tête.position.horizontale = Options_Jeu.Plateau_horizontale + 1 Then
                perdu = True
            End If

            'Affichage de la tête après déplacement
            If perdu = False Then
                Plateau.Item(serpent.Tête.position.emplacement).BackgroundImage = GetTETEB()
            End If

            '--------------Vers la gauche
        ElseIf direction.Gauche = True Then

            'Si le serpent se prend le mur de Gauche
            If serpent.Tête.position.verticale = 0 Then
                perdu = True
            End If

            'Affichage de la tête après déplacement
            If perdu = False Then
                Plateau.Item(serpent.Tête.position.emplacement).BackgroundImage = GetTETEG()
            End If

            '-------------- Vers la droite
        ElseIf direction.Droite = True Then

            'Si le serpent se prend le mur de droite
            If serpent.Tête.position.verticale = Options_Jeu.Plateau_Verticale + 1 Then
                perdu = True
            End If

            'Affichage de la tête après déplacement
            If perdu = False Then
                Plateau.Item(serpent.Tête.position.emplacement).BackgroundImage = GetTETED()
            End If

        End If


        For i = 1 To (Options_Jeu.Plateau_horizontale * Options_Jeu.Plateau_Verticale)

            'Actualisation de la position de tous le corp
            Dim corp As Boolean = False

            For y = 0 To ((Options_Jeu.Plateau_horizontale * Options_Jeu.Plateau_Verticale) - 2)

                If i = serpent.Corps(y).position.emplacement Then
                    corp = True
                End If

            Next

            'Actualise les espace vide du plateau
            If i <> Nourriture.position.emplacement Then
                If i <> serpent.Tête.position.emplacement Then
                    If corp = False Then
                        Plateau.Item(i).BackgroundImage = Nothing
                    End If
                End If
            End If

        Next

        'On vérifi si on ne s'est pas foncer dedans
        If perdu = False Then
            perdu = serpent_colision()
        End If

        If perdu = True Then
            Exit Sub
        End If

        If serpent.Tête.position.emplacement = Nourriture.position.emplacement Then
            Nourriture_Manger()
        End If

    End Sub



    Sub Nourriture_Initialise()

        'on génère un nouvel emplacement pour la nourriture
        While serpent_colision(True)
            Randomize()
            Nourriture.position.emplacement = Int(Rnd() * (Options_Jeu.Plateau_horizontale * Options_Jeu.Plateau_Verticale)) + 1
        End While

        'Récupère la position horizontale
        Nourriture.position.horizontale = Pos_Horizontale(Nourriture.position.emplacement)
        'Récupère la position verticale
        Nourriture.position.verticale = Pos_Verticale(Nourriture.position.emplacement)

        'Affichage de l'image
        Select Case Options_Jeu.Nourriture
            Case 0
                Plateau.Item(Nourriture.position.emplacement).BackgroundImage =
                Global.Snake_Twitch_Plays.My.Resources.SOURIS
            Case 1
                Plateau.Item(Nourriture.position.emplacement).BackgroundImage =
                Global.Snake_Twitch_Plays.My.Resources.POMME
            Case Else
                Plateau.Item(Nourriture.position.emplacement).BackgroundImage =
                Global.Snake_Twitch_Plays.My.Resources.SOURIS
        End Select

    End Sub

    Sub Nourriture_avance(ByVal Direction As Struct_Directions)

        'Calcul la nouvelle position et l'enregistre dans une variable temporaire
        Dim Temp_Pos As Struct_Position = Nourriture.position
        Temp_Pos = Deplacement(Nourriture.position, Direction)

        If Nourriture_colision(Temp_Pos) = False Then

            If Direction.Haut = True Then

                'Si le serpent fonce dans le mur du Haut
                If Temp_Pos.horizontale = 0 Then
                    Exit Sub
                End If

            ElseIf Direction.bas = True Then

                'Si le serpent fonce dans le mur du Haut
                If Temp_Pos.horizontale = Options_Jeu.Plateau_horizontale + 1 Then
                    Exit Sub
                End If

            ElseIf Direction.Gauche = True Then

                'Si le serpent fonce dans le mur du Haut
                If Temp_Pos.verticale = 0 Then
                    Exit Sub
                End If

            ElseIf Direction.Droite = True Then

                'Si le serpent fonce dans le mur du Haut
                If Temp_Pos.verticale = Options_Jeu.Plateau_Verticale + 1 Then
                    Exit Sub
                End If

            End If

            'On supprime l'image de la nourriture à l'emplacement actuelle
            Plateau.Item(Nourriture.position.emplacement).BackgroundImage = Nothing

            'Déplacement de la souris
            Nourriture.position = Temp_Pos

            'Affichage de l'image de la nourriture au nouvel emplacement
            Select Case Options_Jeu.Nourriture
                Case 0
                    Plateau.Item(Nourriture.position.emplacement).BackgroundImage =
                Global.Snake_Twitch_Plays.My.Resources.SOURIS
                Case 1
                    Plateau.Item(Nourriture.position.emplacement).BackgroundImage =
                Global.Snake_Twitch_Plays.My.Resources.POMME
                Case Else
                    Plateau.Item(Nourriture.position.emplacement).BackgroundImage =
                Global.Snake_Twitch_Plays.My.Resources.SOURIS
            End Select


        End If

    End Sub


    Sub Nourriture_Manger()

        'score.Text = CStr(CInt(score.Text) + Options_Jeu.Nb_Points)
        If Joueur <> "RitnTV" Then
            Actualise_Leaderboard(Joueur, Options_Jeu.Nb_Points)
        End If

        serpent.Corps(serpent.Taille).plein = 1
        serpent.Taille += 1
        Nourriture_Initialise()
        LB_Status.Text = Joueur & " mange la "

        Select Case Options_Jeu.Nourriture
            Case Enum_Nourriture.Pomme
                LB_Status.Text &= "Pomme ! (+10)"
            Case Enum_Nourriture.Souris
                LB_Status.Text &= "Souris ! (+10)"
        End Select

    End Sub

    Sub Routine_perdu()
        perdu = False
        Actualisation.Enabled = False
        If Options_Jeu.Restart = True Then
            Recommencer()
        End If
        LB_Status.Text = Joueur & " s'est mangé un truc ! (-5)"
        Actualise_Leaderboard(Joueur, 5, False)
    End Sub

    Sub Creation_Plateau()

        For horizontale = 0 To Options_Jeu.Plateau_horizontale - 1

            For verticale = 0 To Options_Jeu.Plateau_Verticale - 1

                Dim Case0 As New Panel
                With Case0
                    .Size = New System.Drawing.Size(20, 20)
                    .BorderStyle = System.Windows.Forms.BorderStyle.None
                    .BackColor = System.Drawing.Color.FromArgb(4, 0, 13)
                    .Location = New System.Drawing.Point((verticale * 20) + 2, (horizontale * 20) + 2)
                    .BackgroundImageLayout = ImageLayout.Zoom
                End With
                Plateau.Add(Case0, "Case" & CStr((verticale + (horizontale * Options_Jeu.Plateau_Verticale)) + 1))
                Me.Panel_Bordure.Controls.Add(Case0)
            Next
        Next

    End Sub


    'Actualise les points des joueurs
    Sub Actualise_Leaderboard(ByVal pseudo As String, ByVal points As Integer, Optional positif As Boolean = True)

        'Taille du leaderboard
        Dim longueur As Integer = Leaderboard.Count
        Dim placement As Integer = 0
        Dim present As Boolean = False
        Dim Joueur_Actuel As Struct_Joueur

        'ecris le pseudo du joueur à actualiser dans Joueur_Actuel.pseudo
        Joueur_Actuel.Pseudo = pseudo


        'efface la liste afficher
        List_Joueurs.Items.Clear()
        List_Points.Items.Clear()


        'Taille du leaderboard différent de vide
        If longueur <> 0 Then

            Dim Pl As Struct_Joueur

            'Vérifie que le pseudo est présent ou non dans le leaderboard
            Pl = Retrouve_Joueur(Leaderboard, Joueur_Actuel)

            'Si le Joueur existe deja dans le leaderboard
            If Pl.Pseudo <> Nothing Then
                Joueur_Actuel = Pl

                'Supprime le Joueur_Actuel de la liste
                Leaderboard.Remove(Pl)

                'On met à jour les points du joueur
                If positif = True Then
                    Joueur_Actuel.Points += points
                Else
                    Joueur_Actuel.Points -= points
                    If Joueur_Actuel.Points < 0 Then
                        Joueur_Actuel.Points = 0
                    End If
                End If

                'Je récupère la nouvelle taille de leaderboard
                longueur = Leaderboard.Count
                'Jeu déclare la variable trouver à false
                Dim trouver As Boolean = False

                For i = 0 To longueur - 1
                    'Si leaderboard(i) à moins de points ou un nombres égales que joueur_actuel
                    'alors j'insert à cette emplacement le joueur_actuel
                    If Leaderboard(i).Points <= Joueur_Actuel.Points Then
                        Leaderboard.Insert(i, Joueur_Actuel)
                        'On met à Vrai la variable trouver
                        trouver = True
                        Exit For
                    End If

                Next

                'Si le joueur a un nombre de points inferieur à tous le monde on l'ajoute donc à la fin.
                If trouver = False Then
                    Leaderboard.Add(Joueur_Actuel)
                End If


            Else
                'Si le Joueur n'existe pas dans le leaderboard on l'ajoute à la fin de la liste
                If positif = True Then
                    Joueur_Actuel.Points = points
                    Leaderboard.Add(Joueur_Actuel)
                End If
            End If

        Else
            'Leaderboard vide, alors on met un nouveau joueur à la premiere place
            If positif = True Then
                Joueur_Actuel.Points = points
                Leaderboard.Add(Joueur_Actuel)
            End If
        End If

            longueur = Leaderboard.Count

        'Affichage du leaderboard
        For i = 0 To longueur - 1
            'Affiche que les 15 premiers
            If i <= 14 Then
                If Leaderboard(i).Points > 0 Then
                    List_Joueurs.Items.Add(Leaderboard(i).Pseudo)
                    List_Points.Items.Add(Leaderboard(i).Points)
                End If
            Else
                Exit For
            End If
        Next


    End Sub

    Sub Sauvegarde_Leaderboard()

        Dim Liste_Pseudo As New List(Of String)
        Dim Liste_Score As New List(Of String)

        'Enregistre le contenu du leaderboard avant de quitter
        If Leaderboard.Count <> 0 Then

            For i = 0 To Leaderboard.Count - 1
                Liste_Pseudo.Add(Leaderboard(i).Pseudo)
                Liste_Score.Add(Leaderboard(i).Points)
            Next

        End If

        'Sauvegarde du Leaderboard
        Dim contenu(Leaderboard.Count - 1) As String
        Liste_Pseudo.CopyTo(contenu)
        EcritTout(chemin(4), contenu)

        Liste_Score.CopyTo(contenu)
        EcritTout(chemin(5), contenu)

    End Sub

    Sub Chargement_Leaderboard()

        Dim joueur_actuel As Struct_Joueur
        Dim contenu() As String

        'recupération de tous les pseudos sauvegardés
        contenu = ExisteLitTout(chemin(4))

        If contenu.Count <> 0 Then

            'Charge le leaderboard un à un
            For i = 0 To contenu.Count - 1
                joueur_actuel.Pseudo = contenu(i)
                Leaderboard.Add(joueur_actuel)
            Next

            'récupération des scores sauvegardés
            contenu = ExisteLitTout(chemin(5))

            'Charge le leaderboard un à un
            For i = 0 To contenu.Count - 1
                joueur_actuel = Leaderboard(i)
                joueur_actuel.Points = CInt(contenu(i))
                Leaderboard(i) = joueur_actuel
            Next

            'Affichage du leaderboard
            For i = 0 To Leaderboard.Count - 1
                'Affiche que les 15 premiers
                If i <= 14 Then
                    If Leaderboard(i).Points > 0 Then
                        List_Joueurs.Items.Add(Leaderboard(i).Pseudo)
                        List_Points.Items.Add(Leaderboard(i).Points)
                    End If
                Else
                    Exit For
                End If
            Next

        End If

    End Sub


#End Region



#Region "Lancement de l'application"

    Sub Acquisition_Chemin()

        For i = 0 To 100
            chemin(i) = Nothing
        Next


        chemin(0) = My.Application.Info.DirectoryPath & "\"     'Emplacement de l'app (d'oùu elle est lancé)
        chemin(1) = chemin(0) & "cmd.txt"                       'fichier cmd.txt, fichier à lire pour lire les cmd du snake
        chemin(2) = chemin(0) & "sources\"                      'Emplacement du dossier "sources"
        chemin(3) = chemin(0) & "cmd_mouse.txt"                 ''fichier cmd_mouse.txt, fichier à lire pour lire les cmd du snake
        chemin(4) = chemin(0) & "save.txt"                      'Fichier de sauvegarde de la liste des joueurs par rapport à leur classement
        chemin(5) = chemin(0) & "points.txt"                    'Fichier de sauvegarde des points des jours par rapport à leur classement
        chemin(6) = Nothing
        chemin(7) = Nothing
        chemin(8) = Nothing
        chemin(9) = Nothing
        chemin(10) = Nothing
        chemin(11) = Nothing




        chemin(100) = Nothing
    End Sub

    Sub Launcher()

        Acquisition_Chemin()

        'Créer le dossier sources
        ExistePasLeCreer(chemin(2))
        cheminCommandeTXT = chemin(1)
        cheminOriginal = chemin(2)


        Version = "©2019 - Version Beta 0.4.0"
        Actualisation.Enabled = False
        LB_Version.Text = Version
        LB_Status.Text = Nothing


        'Donne la taille max du serpent
        ReDim serpent.Corps(0 To 808)

        'N'affiche pas le Status Nourriture
        Panel_Status_Nourriture.Visible = False

        'Suppression fichier TXT : cmd.txt
        If File.Exists(cheminCommandeTXT) Then
            File.Delete(cheminCommandeTXT)
        End If

        'Configuration de la variable Direction_Param
        Config_Direction_Param()


        'Récupération de la dernière sauvegarde du leaderboard
        Chargement_Leaderboard()


    End Sub

#End Region



#Region "Application"


    'Clique souris (Début du déplacement de la fenêtre)
    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown

        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = True
            MouseDownX = e.X
            MouseDownY = e.Y
        End If
    End Sub
    'Relachement du clique souris (Déplacement terminé)
    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseUp

        If e.Button = MouseButtons.Left Then
            IsFormBeingDragged = False
        End If
    End Sub
    'Déplacement de la fenêtre
    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove

        If IsFormBeingDragged Then
            Dim temp As Point = New Point()

            temp.X = Me.Location.X + (e.X - MouseDownX)
            temp.Y = Me.Location.Y + (e.Y - MouseDownY)
            Me.Location = temp
            temp = Nothing
        End If
    End Sub



    'LANCEMENT DU JEU
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Launcher()
    End Sub



    'Sur le changement de Status, on lance le timer
    Private Sub LB_Status_textChanged(sender As Object, e As EventArgs) Handles LB_Status.TextChanged
        Timer_Status.Enabled = True
    End Sub

    'A la fin du timer, on efface LB_Status et arrete le timer
    Private Sub Timer_Status_Tick(sender As Object, e As EventArgs) Handles Timer_Status.Tick
        Timer_Status.Enabled = False
        LB_Status.Text = Nothing
    End Sub



    'OPTIONS DE JEU et DEMARRAGE
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles LB_by_START.Click
        Options.Show()
    End Sub

    'QUITTER LE JEU
    Private Sub LB_Quitter_Click(sender As Object, e As EventArgs) Handles LB_Quitter.Click

        Actualisation.Enabled = False
        Sauvegarde_Leaderboard()

        'Fermeture
        Me.Close()
    End Sub



#End Region


#Region "Panel InGame"


    'Gestion lié au déplacement de la Nourriture (Souris) avec la commande !mouse
    Sub Gestion_Cmd_Souris()

        'Commande tchat :
        '$overwritefile("D:\Users\Ritn\source\repos\Snake Twitch Plays\Snake Twitch Plays\bin\Debug\cmd_mouse.txt","$username|$msg")

        'Déclaration des variables
        Dim Captureflux As String = Nothing
        Dim commande As String



        'Si le fichier txt existe
        If File.Exists(chemin(3)) Then


            Try
                'Lecture du fichier txt
                Dim lecture As New StreamReader(chemin(3))
                Captureflux = lecture.ReadLine
                lecture.Close()
            Catch
                Exit Sub
            End Try

            'Suppression fichier TXT : cmd_mouse.txt
            If File.Exists(chemin(3)) Then
                File.Delete(chemin(3))
            End If




            'Traitement de la commande
            If Captureflux <> Nothing Then

                    'Init des parametres commandes
                    Joueur = Nothing
                    commande = Nothing


                    'Recuperation du Joueur
                    Joueur = Mid(Captureflux, 1, Captureflux.IndexOf("|"))
                    commande = Mid(Captureflux, Len(Joueur) + 2)

                    'Prise en compte du déplacement du serpent
                    Select Case commande(0)

                    Case "haut", "h", "z"

                        'Déplacement autorisé seulement si les 30seconde sont écoulés et que le joueur a JOIN
                        If TimerNourriture.Enabled = False Then
                            If join = True Then
                                Nourriture_avance(direction_Param(1))
                                TimerNourriture.Enabled = True
                                Panel_Status_Nourriture.Visible = False
                            End If
                        End If

                    Case "bas", "b", "s"

                        'Déplacement autorisé seulement si les 30seconde sont écoulés et que le joueur a JOIN
                        If TimerNourriture.Enabled = False Then
                            If join = True Then
                                Nourriture_avance(direction_Param(2))
                                TimerNourriture.Enabled = True
                                Panel_Status_Nourriture.Visible = False
                            End If
                        End If

                    Case "gauche", "g", "q"

                        'Déplacement autorisé seulement si les 30seconde sont écoulés et que le joueur a JOIN
                        If TimerNourriture.Enabled = False Then
                            If join = True Then
                                Nourriture_avance(direction_Param(3))
                                TimerNourriture.Enabled = True
                                Panel_Status_Nourriture.Visible = False
                            End If
                        End If

                    Case "droite", "d"

                        'Déplacement autorisé seulement si les 30seconde sont écoulés et que le joueur a JOIN
                        If TimerNourriture.Enabled = False Then
                            If join = True Then
                                Nourriture_avance(direction_Param(4))
                                TimerNourriture.Enabled = True
                                Panel_Status_Nourriture.Visible = False
                            End If
                        End If

                    Case "join"

                            If Joueur = "RitnTV" Then
                                join = True
                            End If
                            TimerNourriture.Enabled = True
                            Panel_Status_Nourriture.Visible = False

                        Case "unjoin"

                            If Joueur = "RitnTV" Then
                                join = False
                            End If

                        Case "score"

                            Dim Joueur_actuel, Pl As Struct_Joueur
                            Joueur_actuel.Pseudo = Joueur
                            Pl = Retrouve_Joueur(Leaderboard, Joueur_actuel)

                            If Pl.Pseudo <> Nothing Then
                                Joueur_actuel = Pl
                            End If

                            'Affiche le nombre de point sur demande
                            LB_Status.Text = Joueur_actuel.Pseudo & " : " & Joueur_actuel.Points

                        Case Else
                            'Ne rien faire
                    End Select


                End If


            End If



    End Sub


    'Gestion lié au déplacement du serpent avec la commande !snake
    Sub Gestion_Cmd_Serpent()

        'Si la valeur de Coups est supérieur à 0 alors je décrémente de 1
        If serpent.Coups > 0 Then
            serpent.Coups -= 1
        End If



        'Déclaration des variables
        Dim captureflux As String


        Try

            'Commande tchat :
            '$overwritefile("D:\Users\Ritn\source\repos\Snake Twitch Plays\Snake Twitch Plays\bin\Debug\cmd.txt","$username|$msg")


            If serpent.Coups <= 0 Then

                If File.Exists(cheminCommandeTXT) Then


                    Try
                        'Lecture du fichier txt
                        Dim lecture As New StreamReader(cheminCommandeTXT)
                        captureflux = lecture.ReadLine
                        lecture.Close()
                    Catch
                        Exit Sub
                    End Try


                    'Suppression fichier TXT : cmd.txt
                    If File.Exists(cheminCommandeTXT) Then
                        File.Delete(cheminCommandeTXT)
                    End If

                    'Traitement de la commande
                    If captureflux <> Nothing Then

                        'Init des parametres commandes
                        Joueur = Nothing
                        commande(0) = Nothing
                        commande(1) = Nothing

                        'Recuperation du Joueur
                        Joueur = Mid(captureflux, 1, captureflux.IndexOf("|"))
                        captureflux = Mid(captureflux, Len(Joueur) + 2)
                        'Recuperation de Param1 et 2
                        If captureflux.IndexOf(" ") <> -1 Then
                            commande(0) = Mid(captureflux, 1, captureflux.IndexOf(" "))
                            captureflux = Mid(captureflux, Len(commande(0)) + 2)
                            commande(1) = captureflux
                        Else
                            commande(0) = captureflux
                        End If

                        'Je ne peux pas jouer de déplacement court ou pendant un temps de 30sec
                        'If Joueur = "RitnTV" Then

                        '    If commande(1) <> Nothing Then
                        '        Exit Sub
                        '    Else
                        '        If TimerNourriture.Enabled = True Then
                        '            Exit Sub
                        '        End If
                        '    End If

                        'End If


                        'Prise en compte du déplacement du serpent
                        Select Case commande(0)

                            Case "haut", "h", "z"

                                If Not direction_Param(5).Equals(direction_Param(2)) Then

                                    LB_Flux.Text = Joueur & " : " & "haut"
                                    Direction_Change(Struct_Directions.Sens.Haut)
                                    Serpent_avance()

                                    If perdu = True Then
                                        Routine_perdu()
                                    End If

                                    'Sauvegarde de la direction
                                    direction_Param(5) = direction

                                End If

                            Case "bas", "b", "s"

                                If Not direction_Param(5).Equals(direction_Param(1)) Then

                                    LB_Flux.Text = Joueur & " : " & "bas"
                                    Direction_Change(Struct_Directions.Sens.Bas)
                                    Serpent_avance()

                                    If perdu = True Then
                                        Routine_perdu()
                                    End If

                                    'Sauvegarde de la direction
                                    direction_Param(5) = direction

                                End If

                            Case "gauche", "g", "q"

                                If Not direction_Param(5).Equals(direction_Param(4)) Then

                                    LB_Flux.Text = Joueur & " : " & "gauche"
                                    Direction_Change(Struct_Directions.Sens.Gauche)
                                    Serpent_avance()

                                    If perdu = True Then
                                        Routine_perdu()
                                    End If

                                    'Sauvegarde de la direction
                                    direction_Param(5) = direction

                                End If

                            Case "droite", "d"

                                If Not direction_Param(5).Equals(direction_Param(3)) Then

                                    LB_Flux.Text = Joueur & " : " & "droite"
                                    Direction_Change(Struct_Directions.Sens.Droite)
                                    Serpent_avance()

                                    If perdu = True Then
                                        Routine_perdu()
                                    End If

                                    'Sauvegarde de la direction
                                    direction_Param(5) = direction

                                End If

                            Case "score"

                                Dim Joueur_actuel, Pl As Struct_Joueur
                                Joueur_actuel.Pseudo = Joueur
                                Pl = Retrouve_Joueur(Leaderboard, Joueur_actuel)

                                If Pl.Pseudo <> Nothing Then
                                    Joueur_actuel = Pl
                                End If

                                'Affiche le nombre de point sur demande
                                LB_Status.Text = Joueur_actuel.Pseudo & " : " & Joueur_actuel.Points


                            Case "■▲Χ●■■■▲●R2R2PavéTactile"
                                LB_Flux.Text = Joueur & " : " & "ARRETE DE CHEAT !!!"
                                'Ne rien faire

                            Case Else

                                LB_Flux.Text = "Commande invalide !"

                        End Select

                        'Prise en compte du paramètre suivant dans la commande
                        Select Case commande(1)

                            Case "2", "3", "4", "5"
                                'Si le second paramètre est un chiffre entre 2 et 5 alors
                                'le serpent avance de pusieurs case à la suite.
                                'If Joueur <> "RitnTV" Then
                                serpent.Coups = CInt(commande(1))
                                Actualise_Leaderboard(Joueur, CInt(commande(1)), False)
                                'End If

                            Case Else
                                serpent.Coups = 1

                                ''Si c'est moi qui joue
                                'If Joueur = "RitnTV" Then
                                '    Actualise_Leaderboard(Joueur, 1)
                                '    TimerNourriture.Enabled = True
                                '    Panel_Status_Nourriture.Visible = False
                                'End If

                        End Select

                    End If

                End If

            Else

                Serpent_avance()

                If perdu = True Then
                    Routine_perdu()
                End If

            End If

        Catch ex As Exception

            'MsgBox(ex.ToString)
            LB_Flux.Text = "Erreur !!!"

        End Try


    End Sub


    'Toutes les 500 millisecondes, lecture du fichier : cmd.txt
    Private Sub Actualisation_Tick(sender As Object, e As EventArgs) Handles Actualisation.Tick

        Gestion_Cmd_Serpent()

    End Sub

    'Toutes les 500 millisecondes, lecture du fichier : cmd_mouse.txt
    Private Sub Actualisation_Nourriture_Tick(sender As Object, e As EventArgs) Handles Actualisation_Nourriture.Tick

        Gestion_Cmd_Souris()

    End Sub


    'A la fin du temps sans déplacement de la souris :
    Private Sub TimerNourriture_Tick(sender As Object, e As EventArgs) Handles TimerNourriture.Tick
        TimerNourriture.Enabled = False
        Panel_Status_Nourriture.Visible = True
    End Sub



#End Region

End Class
