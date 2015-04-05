Imports System.Text
Imports System.IO
Public Class Principal
    Inherits System.Web.UI.Page

    Public caracter As Char
    Public cadena As String
    Public strresultado As String
    Public strresultado2 As String
    Public arreglo As Char()
    Public lista1 As New List(Of lista)
    Public lista2 As New List(Of lista)
    Public Shared Property Today As DateTime


    Public tokn As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub





    Public Sub analizadorlex(ByVal cad As String)
        Dim i As Integer = 0
        Dim est As Integer = 0
        arreglo = cad.ToCharArray


        While (i < arreglo.Count)


            Select Case est

                ' ir a los primeros estados

                Case 0

                    If (Asc(arreglo(i)) = 35) Then
                        est = 1

                    ElseIf (arreglo(i) = " " Or Asc(arreglo(i)) > 1 And Asc(arreglo(i)) < 33) Then
                        i = i + 1
                        est = 0

                    ElseIf (Char.IsLetter(arreglo(i))) Then
                        est = 1
                    ElseIf (Char.IsDigit(arreglo(i))) Then
                        est = 1
                    ElseIf (Asc(arreglo(i)) = 60) Then
                        i = i + 1
                        est = 2
                        tokn = "<"
                    ElseIf (Char.IsSymbol(arreglo(i)) Or arreglo(i) = "/" Or arreglo(i) = "*" Or arreglo(i) = "<" Or arreglo(i) = ">") Then
                        est = 4
                    ElseIf (arreglo(i) = "{" Or arreglo(i) = "}") Then
                        est = 5
                    ElseIf (arreglo(i) = ";") Then
                        est = 6

                    ElseIf (Asc(arreglo(i)) = 34) Then
                        i = i + 1
                        est = 7
                    ElseIf (arreglo(i) = "(" Or arreglo(i) = ")") Then
                        est = 8
                    ElseIf (arreglo(i) = ",") Then
                        est = 9
                    Else
                        est = 3
                    End If


                    ' estado palabra ---> 1
                Case 1

                    If (Asc(arreglo(i)) = 60 Or (arreglo(i)) = " " Or (arreglo(i)) = "(" Or (arreglo(i)) = ")" Or arreglo(i) = "{" Or arreglo(i) = "}" Or Asc(arreglo(i)) = 10 Or arreglo(i) = ";" Or arreglo(i) = "+" Or Asc(arreglo(i)) > 1 And Asc(arreglo(i)) < 33) Then

                        strresultado = strresultado + tokn & " --> token" & vbCrLf
                        lista1.Add(New lista With {.token = tokn, .tipo = "token"})

                        tokn = ""
                        est = 0


                    ElseIf (Char.IsLetterOrDigit(arreglo(i)) Or Asc(arreglo(i)) = 35 Or arreglo(i) = "(") Then

                        tokn = tokn + arreglo(i)
                        i = i + 1

                    Else
                        est = 3
                    End If

                    ' estado etiqueta --> 2 

                Case 2


                    If (Char.IsLetter(arreglo(i)) Or arreglo(i) = ".") Then
                        tokn = tokn + arreglo(i)
                        i = i + 1
                    ElseIf (arreglo(i) = ">") Then
                        i = i + 1
                        tokn = tokn + ">"
                        strresultado += tokn + " --> etiqueta" + vbCrLf
                        lista1.Add(New lista With {.token = tokn, .tipo = "etiqueta"})

                        i = i + 1

                        est = 0
                        tokn = ""
                    ElseIf (arreglo(i) = " ") Then
                        strresultado = strresultado + tokn + " --> operador" + vbCrLf
                        lista1.Add(New lista With {.token = tokn, .tipo = "operador"})

                        tokn = ""

                        est = 0
                        i = i + 1
                    Else
                        est = 3
                    End If

                    ' estado error --> 3
                Case 3

                    ' If (arreglo(i) = " " Or arreglo(i) = ">") Then
                    tokn = tokn + arreglo(i)
                    strresultado = strresultado + tokn + " --> error" + vbCrLf
                    lista1.Add(New lista With {.token = tokn, .tipo = "error"})

                    tokn = ""

                    est = 0
                    i = i + 1

                    '  Else
                    ' tokn = tokn + arreglo(i)
                    'i = i + 1

                    'End If

                Case 4
                    If (arreglo(i) = "+") Then
                        tokn = tokn + arreglo(i)
                        i = i + 1
                        If (arreglo(i) = "+") Then
                            tokn = tokn + arreglo(i)
                            strresultado = strresultado + tokn + " --> masuno" + vbCrLf
                            lista1.Add(New lista With {.token = tokn, .tipo = "masuno"})

                            tokn = ""

                            est = 0
                            i = i + 1
                        Else
                            lista1.Add(New lista With {.token = tokn, .tipo = "operador"})
                            strresultado = strresultado + tokn + " --> operador" + vbCrLf
                            tokn = ""

                            est = 0

                        End If

                    Else
                        tokn = tokn + arreglo(i)
                        strresultado = strresultado + tokn + " --> operador" + vbCrLf
                        lista1.Add(New lista With {.token = tokn, .tipo = "operador"})

                        tokn = ""

                        est = 0
                        i = i + 1
                    End If


                Case 5
                    tokn = tokn + arreglo(i)
                    strresultado = strresultado + tokn + " --> llave" + vbCrLf
                    lista1.Add(New lista With {.token = tokn, .tipo = "llave"})

                    tokn = ""

                    est = 0
                    i = i + 1

                Case 6
                    tokn = tokn + arreglo(i)
                    strresultado = strresultado + tokn + " --> fincomando" + vbCrLf
                    lista1.Add(New lista With {.token = tokn, .tipo = "fincomando"})

                    tokn = ""

                    est = 0
                    i = i + 1

                Case 7
                    If (Asc(arreglo(i)) = 34) Then


                        strresultado = strresultado + tokn & " --> string" & vbCrLf
                        lista1.Add(New lista With {.token = tokn, .tipo = "string"})

                        i = i + 1
                        tokn = ""
                        est = 0

                    ElseIf (Char.IsLetterOrDigit(arreglo(i)) Or arreglo(i) = " " Or arreglo(i) = ":" Or arreglo(i) = "%" Or arreglo(i) = "," Or arreglo(i) = ".") Then
                        tokn = tokn + arreglo(i)
                        i = i + 1

                    Else
                        est = 3
                    End If


                Case 8
                    tokn = tokn + arreglo(i)
                    strresultado = strresultado + tokn + " --> parentesis" + vbCrLf
                    lista1.Add(New lista With {.token = tokn, .tipo = "parentesis"})

                    tokn = ""

                    est = 0
                    i = i + 1

                Case 9
                    tokn = tokn + arreglo(i)
                    strresultado = strresultado + tokn + " --> coma" + vbCrLf
                    lista1.Add(New lista With {.token = tokn, .tipo = "coma"})

                    tokn = ""

                    est = 0
                    i = i + 1

            End Select

        End While

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


        For Each item As lista In lista1
            If (item.tipo = "token") Then
                If (item.token = "#include") Then
                    lista2.Add(New lista With {.token = item.token, .tipo = "libreria"})
                ElseIf (item.token = "1" Or item.token = "2" Or item.token = "3" Or item.token = "4" Or item.token = "5" Or item.token = "6" Or item.token = "7" Or item.token = "8" Or item.token = "9" Or item.token = "0") Then
                    lista2.Add(New lista With {.token = item.token, .tipo = "num"})
                ElseIf (item.token = "int") Then
                    lista2.Add(New lista With {.token = item.token, .tipo = "entero"})
                ElseIf (item.token = "float") Then
                    lista2.Add(New lista With {.token = item.token, .tipo = "float"})
                ElseIf (item.token = "printf") Then
                    lista2.Add(New lista With {.token = item.token, .tipo = "imprimir"})
                ElseIf (item.token = "time") Then
                    lista2.Add(New lista With {.token = item.token, .tipo = "hora"})
                ElseIf (item.token = "void") Then
                    lista2.Add(New lista With {.token = item.token, .tipo = "void"})
                Else
                    lista2.Add(item)
                End If
            Else
                lista2.Add(item)
            End If
        Next

        strresultado = ""

        For Each ele As lista In lista2
            strresultado = strresultado + ele.token + " --> " + ele.tipo + vbCrLf
        Next

        txtlex.Text = strresultado

        strresultado = ""


    End Sub



    Public Sub analizadorsin()

        Dim i As Integer = 0
        Dim est As Integer = 0


        Dim tabla As New List(Of simbolos)

        Dim currentTime As Date
        currentTime = TimeOfDay

        Dim m As Integer = 0
        Dim n As Integer = 0
        Dim met As String = ""
        Dim esta As Integer = 0
        Dim strconsola As String = ""

        While (m < lista2.Count)

            Select Case esta

                Case 0
                    If (lista2(m).tipo = "libreria") Then
                        esta = 1
                        m = m + 1

                    ElseIf (lista2(m).tipo = "void") Then
                        esta = 2
                        m = m + 1
                    ElseIf (lista2(m).token = "}") Then
                        strresultado2 = strresultado2 + "método terminado" + vbCrLf
                        m = m + 1
                    Else
                        strresultado2 = strresultado2 + "error" + vbCrLf
                        m = m + 1
                    End If

                Case 1
                    If (lista2(m).token.Equals("<stdio.h>")) Then
                        strresultado2 = strresultado2 + "incluye declaracion de var e imprimir en pant" + vbCrLf
                        m = m + 1
                        esta = 0
                    ElseIf (lista2(m).token.Equals("<conio.h>")) Then
                        strresultado2 = strresultado2 + "incluye operaciones aritmeticas" + vbCrLf
                        m = m + 1
                        esta = 0
                    ElseIf (lista2(m).token.Equals("<string.h>")) Then
                        strresultado2 = strresultado2 + "incluye caracteres y strings" + vbCrLf
                        m = m + 1
                        esta = 0
                    ElseIf (lista2(m).token.Equals("<time.h>")) Then
                        strresultado2 = strresultado2 + "incluye hora del sistema" + vbCrLf
                        m = m + 1
                        esta = 0
                    Else
                        strresultado2 = strresultado2 + "Error al incluir la libreria" + lista2(m).token + vbCrLf
                        m = m + 1
                        esta = 0
                    End If

                Case 2

                    met = lista2(m).token
                    m = m + 1
                    If (lista2(m).tipo.Equals("parentesis")) Then
                        m = m + 1
                        If (lista2(m).tipo.Equals("parentesis")) Then
                            m = m + 1
                            If (lista2(m).token.Equals("{")) Then
                                m = m + 1
                                esta = 3
                                strresultado2 = strresultado2 + "Se inicio metodo" + vbCrLf
                            Else
                                strresultado2 = strresultado2 + "Error en el inicio metodo" + vbCrLf
                                m = m + 1
                                esta = 3
                            End If
                        Else
                            strresultado2 = strresultado2 + "Error segundo parentesis" + vbCrLf
                            met = ""
                            m = m + 1
                            esta = 0
                        End If
                    Else
                        strresultado2 = strresultado2 + "Error primer parentesis" + vbCrLf
                        met = ""
                        m = m + 1
                        esta = 0
                    End If


                Case 3
                    If (lista2(m).token = "int") Then
                        esta = 31
                        m = m + 1
                    ElseIf (lista2(m).token = "float") Then
                        esta = 32
                        m = m + 1
                    ElseIf (lista2(m).token = "for") Then
                        esta = 35
                        m = m + 1
                    ElseIf (lista2(m).token = "while") Then
                        esta = 36
                        m = m + 1
                    ElseIf (lista2(m).token = "if") Then
                        esta = 37
                        m = m + 1
                    ElseIf (lista2(m).tipo = "imprimir") Then
                        esta = 34
                        m = m + 1
                    ElseIf (lista2(m).tipo = "token") Then
                        esta = 33
                    ElseIf (lista2(m).token = "}") Then
                        esta = 0

                    Else
                        strresultado2 = strresultado2 + "invalido dentro del metodo -->" + lista2(m).token + vbCrLf
                        m = m + 1
                        esta = 0

                    End If



                Case 31
                    If (lista2(m).tipo = "token") Then
                        Dim temstr As String
                        temstr = lista2(m).token
                        tabla.Add(New simbolos With {.nombre = lista2(m).token, .tipo = "int", .location = "Local", .metodo = met})
                        m = m + 1
                        If (lista2(m).token = "=") Then
                            m = m + 1
                            If (lista2(m).tipo.Equals("num")) Then
                                For Each item As simbolos In tabla
                                    If (item.nombre = temstr And item.metodo = met) Then
                                        item.valor = lista2(m).token
                                    End If
                                Next
                                m = m + 1
                                If (lista2(m).tipo = "fincomando") Then
                                    m = m + 1
                                    esta = 3
                                Else
                                    esta = 3
                                    m = m + 1
                                    strresultado2 = strresultado2 + "Error punto y coma" + vbCrLf
                                End If

                            Else
                                m = m + 1
                                esta = 3
                                strresultado2 = strresultado2 + "Error valor en " + lista2(m).token + vbCrLf
                            End If
                        End If
                    Else
                        strresultado2 = strresultado2 + "Error primer al iniciar int " + lista2(m).token + vbCrLf
                        esta = 3
                        m = m + 1
                    End If


                Case 32
                    If (lista2(m).tipo = "token") Then
                        Dim temstr As String
                        temstr = lista2(m).token
                        tabla.Add(New simbolos With {.nombre = lista2(m).token, .tipo = "float", .location = "Local", .metodo = met})
                        m = m + 1
                        If (lista2(m).token = "=") Then
                            m = m + 1
                            If (lista2(m).tipo.Equals("num")) Then
                                For Each item As simbolos In tabla
                                    If (item.nombre = temstr And item.metodo = met) Then
                                        item.valor = lista2(m).token


                                    End If
                                Next
                                m = m + 1
                                If (lista2(m).tipo = "fincomando") Then
                                    m = m + 1
                                    esta = 3
                                Else
                                    esta = 3
                                    m = m + 1
                                    strresultado2 = strresultado2 + "Error punto y coma" + vbCrLf
                                End If

                            Else
                                strresultado2 = strresultado2 + "Error valor en " + lista2(m).token + vbCrLf
                                esta = 3
                                m = m + 1
                            End If
                        End If
                    Else
                        strresultado2 = strresultado2 + "Error primer al iniciar float " + lista2(m).token + vbCrLf
                        esta = 3
                        m = m + 1
                    End If


                Case 33

                    Dim temp1 As Integer = 0
                    Dim temp2 As Integer = 0
                    Dim temp3 As Integer = 0
                    Dim tempd3 As Double = 0
                    Dim strmet As String = ""

                    strmet = strmet + lista2(m).token
                    m = m + 1

                    If (lista2(m).token = "(") Then
                        strmet = strmet + lista2(m).token
                        m = m + 1
                        If (lista2(m).token = ")") Then
                            strmet = strmet + lista2(m).token
                            m = m + 1
                            If (lista2(m).token = ";") Then
                                strresultado2 = strresultado2 + "Metodo ejecutado " + strmet + vbCrLf
                                strmet = ""
                                m = m + 1
                                esta = 3
                            Else
                                strmet = ""
                                m = m + 1
                                esta = 3
                            End If
                        Else
                            strmet = ""
                            m = m + 1
                            esta = 3
                        End If

                    Else

                        '''''''''''''''''''''''''''''''''''''''''''''''''' operaciones con int
                        strmet = ""
                        m = m - 1
                        For Each item As simbolos In tabla
                            If (item.nombre = lista2(m).token And item.metodo = met And item.tipo = "int") Then
                                m = m + 1
                                If (lista2(m).token.Equals("=")) Then
                                    m = m + 1
                                    For Each coso As simbolos In tabla
                                        If (coso.nombre = lista2(m).token And coso.metodo = met) Then
                                            temp1 = Convert.ToInt32(coso.valor)
                                        End If
                                    Next
                                    m = m + 2
                                    For Each cosa As simbolos In tabla
                                        If (cosa.nombre = lista2(m).token And cosa.metodo = met) Then
                                            temp2 = Convert.ToInt32(cosa.valor)
                                        End If
                                    Next
                                    m = m - 1
                                    If (lista2(m).token = "+") Then
                                        temp3 = temp1 + temp2
                                        item.valor = temp3.ToString
                                        m = m + 2
                                        If (lista2(m).tipo.Equals("fincomando")) Then
                                            esta = 3
                                            m = m + 1
                                        Else
                                            strresultado2 = strresultado2 + "Error punto y coma" + vbCrLf
                                            esta = 3
                                            m = m + 1
                                        End If
                                    ElseIf (lista2(m).token = "-") Then
                                        temp3 = temp1 - temp2
                                        item.valor = temp3.ToString
                                        m = m + 2
                                        If (lista2(m).tipo.Equals("fincomando")) Then
                                            esta = 3
                                            m = m + 1
                                        Else
                                            strresultado2 = strresultado2 + "Error punto y coma" + vbCrLf
                                            esta = 3
                                            m = m + 1
                                        End If
                                    ElseIf (lista2(m).token = "*") Then
                                        temp3 = temp1 * temp2
                                        item.valor = temp3.ToString
                                        m = m + 2
                                        If (lista2(m).tipo.Equals("fincomando")) Then
                                            esta = 3
                                            m = m + 1
                                        Else
                                            strresultado2 = strresultado2 + "Error punto y coma" + vbCrLf
                                            esta = 3
                                            m = m + 1
                                        End If

                                    Else
                                        strresultado2 = strresultado2 + "Error en operador " + lista2(m).token + vbCrLf
                                        m = m + 2
                                        esta = 3
                                    End If ' terminan los operadores
                                End If   ' termina igual 

                            ElseIf (item.nombre = lista2(m).token And item.metodo = met And item.tipo = "float") Then
                                m = m + 1
                                If (lista2(m).token.Equals("=")) Then
                                    m = m + 1
                                    For Each coso As simbolos In tabla
                                        If (coso.nombre = lista2(m).token And coso.metodo = met) Then
                                            temp1 = Convert.ToInt32(coso.valor)
                                        End If
                                    Next
                                    m = m + 2
                                    For Each cosa As simbolos In tabla
                                        If (cosa.nombre = lista2(m).token And cosa.metodo = met) Then
                                            temp2 = Convert.ToInt32(cosa.valor)
                                        End If
                                    Next
                                    m = m - 1
                                    If (lista2(m).token = "/") Then
                                        tempd3 = temp1 / temp2
                                        item.valor = tempd3.ToString
                                        m = m + 2
                                        If (lista2(m).tipo.Equals("fincomando")) Then
                                            esta = 3
                                            m = m + 1
                                        Else
                                            strresultado2 = strresultado2 + "Error punto y coma" + vbCrLf
                                            esta = 3
                                            m = m + 1
                                        End If

                                    Else
                                        strresultado2 = strresultado2 + "Error en operador " + lista2(m).token + vbCrLf
                                        m = m + 2
                                        esta = 3
                                    End If ' terminan los operadores
                                End If   ' termina igual 
                            End If
                        Next
                    End If



                Case 34

                    If (lista2(m).token = "(") Then
                        m = m + 1
                        If (lista2(m).tipo = "string") Then
                            strconsola = strconsola + lista2(m).token + vbCrLf
                            m = m + 1
                            If (lista2(m).token = ",") Then
                                m = m + 1

                                If (lista2(m).token = "time") Then
                                    m = m + 4
                                    If (lista2(m).token = ";") Then
                                        strconsola = strconsola + currentTime.ToString + vbCrLf
                                    Else
                                        strresultado2 = strresultado2 + "Error en la punto y coma " + lista2(m).token + vbCrLf
                                        esta = 3
                                        m = m + 1
                                    End If
                                Else

                                    For Each item As simbolos In tabla
                                        If (lista2(m).token = item.nombre And met = item.metodo) Then
                                            strconsola = strconsola + item.valor + vbCrLf
                                            m = m + 1
                                        End If
                                    Next

                                End If
                                If (lista2(m).token = ")") Then
                                    m = m + 1
                                    If (lista2(m).token = ";") Then
                                        esta = 3
                                        m = m + 1
                                    Else
                                        strresultado2 = strresultado2 + "Error en la punto y coma " + lista2(m).token + vbCrLf
                                        esta = 3
                                        m = m + 1
                                    End If
                                End If
                            Else
                                strresultado2 = strresultado2 + "Error en la coma " + lista2(m).token + vbCrLf
                                esta = 3
                                m = m + 1
                            End If
                        Else
                            strresultado2 = strresultado2 + "Error en la cadena " + lista2(m).token + vbCrLf
                            esta = 3
                            m = m + 1
                        End If
                    Else
                        strresultado2 = strresultado2 + "Error en parentesis " + lista2(m).token + vbCrLf
                        esta = 3
                        m = m + 1
                    End If

               
                Case 36

                    Dim var As String = ""
                    Dim cont As Integer = 0
                    Dim var2 As String = ""
                    Dim im As String = ""
                    Dim variable As Integer = 0
                    Dim limite As Integer = 0

                    If (lista2(m).token = "(") Then
                        m = m + 1
                        If (lista2(m).tipo = "token") Then
                            var = lista2(m).token.ToString
                            m = m + 1
                            If (lista2(m).token = "<") Then
                                m = m + 1
                                If (lista2(m).tipo = "token") Then
                                    cont = Convert.ToInt32(lista2(m).token)
                                    m = m + 3
                                    If (lista2(m).tipo = "imprimir") Then
                                        m = m + 2
                                        im = lista2(m).token
                                        m = m + 4
                                    Else
                                        strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                                        esta = 3
                                        m = m + 1
                                    End If
                                Else
                                    strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                                    esta = 3
                                    m = m + 1
                                End If
                            Else
                                strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                                esta = 3
                                m = m + 1
                            End If

                        Else
                            strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                            esta = 3
                            m = m + 1
                        End If
                    Else
                        strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                        esta = 3
                        m = m + 1
                    End If

                    For Each coso As simbolos In tabla
                        If (coso.nombre = var And coso.metodo = met) Then
                            variable = Convert.ToInt32(coso.valor)
                        End If
                    Next


                    While (variable < cont)
                        strconsola = strconsola + im + variable.ToString + vbCrLf
                        variable = variable + 1
                    End While


                    For Each coso As simbolos In tabla
                        If (coso.nombre = var And coso.metodo = met) Then
                            coso.valor = variable
                        End If
                    Next
                    m = m + 7
                    esta = 3

                Case 37
                    Dim svalor1 As String = ""
                    Dim svalor2 As String = ""
                    Dim ivalor1 As Integer = 0
                    Dim ivalor2 As Integer = 0
                    Dim svalor3 As String = ""
                    Dim condi As Boolean = False

                    If (lista2(m).token = "(") Then
                        m = m + 1
                        If (lista2(m).tipo = "token") Then
                            svalor1 = lista2(m).token
                            m = m + 1
                            If (lista2(m).token = "=") Then
                                m = m + 1
                                If (lista2(m).token = "=") Then
                                    m = m + 1
                                    If (lista2(m).tipo = "num") Then
                                        ivalor1 = Convert.ToInt32(lista2(m).token)
                                        m = m + 3
                                    Else
                                        strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                                        esta = 3
                                        m = m + 1
                                    End If
                                Else
                                    strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                                    esta = 3
                                    m = m + 1
                                End If
                            Else
                                strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                                esta = 3
                                m = m + 1
                            End If
                        Else
                            strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                            esta = 3
                            m = m + 1
                        End If
                    Else
                        strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                        esta = 3
                        m = m + 1
                    End If

                    For Each coso As simbolos In tabla
                        If (coso.nombre = svalor1 And coso.metodo = met) Then
                            ivalor2 = Convert.ToInt32(coso.valor)
                        End If
                    Next

                    If (ivalor1 = ivalor2) Then
                        If (lista2(m).tipo = "imprimir") Then
                            m = m + 2
                            If (lista2(m).tipo = "string") Then
                                strconsola = strconsola + lista2(m).token + vbCrLf
                                m = m + 4
                                condi = True
                            Else
                                strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                                esta = 3
                                m = m + 1
                            End If
                        Else
                            strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                            esta = 3
                            m = m + 1
                        End If

                    Else
                        condi = False
                        m = m + 6
                    End If


                    If (lista2(m).token = "else" And condi = False) Then
                        m = m + 2
                        If (lista2(m).tipo = "imprimir") Then
                            m = m + 2
                            If (lista2(m).tipo = "string") Then
                                strconsola = strconsola + lista2(m).token + vbCrLf
                                m = m + 4
                                esta = 3
                            Else
                                strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                                esta = 3
                                m = m + 1
                            End If
                        Else
                            strresultado2 = strresultado2 + "Error en " + lista2(m).token + vbCrLf
                            esta = 3
                            m = m + 1
                        End If
                    ElseIf (lista2(m).token = "else" And condi = True) Then
                        m = m + 8
                        esta = 3
                    Else
                        esta = 3
                    End If



            End Select

        End While

        txtsin.Text = strresultado2
        txtconsola.Text = strconsola

        gridtabla.DataSource = tabla
        gridtabla.DataBind()

    End Sub










    Protected Sub btnanalizar_Click(sender As Object, e As EventArgs) Handles btnanalizar.Click
        If file.HasFile = True Then
            txtentrada.Text = file.PostedFile.FileName.ToString()
            Dim extension = Path.GetExtension(file.PostedFile.FileName)

            Try
                If extension.ToLower.ToString() = ".txt" Then
                    Dim carpeta_final As String = Server.MapPath(file.FileName)
                    file.PostedFile.SaveAs(carpeta_final)
                    Dim path1 As String = carpeta_final
                    Dim OLA As StreamReader
                    OLA = New StreamReader(path1)
                    txtentrada.Text = OLA.ReadToEnd

                Else : txtentrada.Text = "Extension incorrecta"

                End If
            Catch ex As Exception
                txtentrada.Text = "no se guardo"
                Return
            End Try

            analizadorlex(txtentrada.Text)
            Try
                analizadorsin()
            Catch ex As Exception

            End Try


        End If
    End Sub

End Class