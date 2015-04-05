Public Class simbolos

    Public onombre As String
    Public ovalor As String
    Public otipo As String
    Public olocation As String
    Public ometodo As String

    Public Sub New()
    End Sub


    Public Sub New(ByVal nombre As String, ByVal valor As String, ByVal tipo As String, ByVal location As String, ByVal metodo As String)
        onombre = nombre
        ovalor = valor
        otipo = tipo
        olocation = location
        ometodo = metodo
    End Sub

    Public Property nombre As String
        Get
            Return onombre
        End Get
        Set(ByVal value As String)
            onombre = value
        End Set
    End Property

    Public Property valor As String
        Get
            Return ovalor
        End Get
        Set(ByVal value As String)
            ovalor = value
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

    Public Property location As String
        Get
            Return olocation
        End Get
        Set(ByVal value As String)
            olocation = value
        End Set
    End Property

    Public Property metodo As String
        Get
            Return ometodo
        End Get
        Set(ByVal value As String)
            ometodo = value
        End Set
    End Property



End Class
