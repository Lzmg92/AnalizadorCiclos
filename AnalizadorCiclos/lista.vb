Public Class lista
    Public otoken As String
    Public otipo As String

    Public Sub New()
    End Sub


    Public Sub New(ByVal token As String, ByVal tipo As String)
        otoken = token
        otipo = tipo
    End Sub

    Public Property token As String
        Get
            Return otoken
        End Get
        Set(ByVal value As String)
            otoken = value
        End Set
    End Property

    Public Property tipo As String
        Get
            Return otipo
        End Get
        Set(ByVal value As String)
            otipo = value
        End Set
    End Property
End Class
