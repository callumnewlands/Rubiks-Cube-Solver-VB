
Public Class StageNotSuccessfulException
    Inherits Exception

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class WriteUnsuccessfulException
    Inherits Exception

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class ReadUnsuccessfulException
    Inherits Exception

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
    Public Sub New()
        MyBase.New()
    End Sub

End Class

Public Class SaveCancelledException
    Inherits Exception

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
    Public Sub New()
        MyBase.New()
    End Sub

End Class