
'The only reason this is a class and not just a simple boolean
'variable is that it needed a WithEvents declaration and its
'own event.
Public Class Command
    Private mValue As Boolean
    Public Event VariableChanged(ByVal mvalue As Integer)

    Public Property Command() As Integer
        Get
            Return mValue
        End Get
        Set(ByVal value As Integer)
            mValue = value
            RaiseEvent VariableChanged(mValue)
        End Set
    End Property

    Public Sub New()
        mValue = True
    End Sub

End Class
