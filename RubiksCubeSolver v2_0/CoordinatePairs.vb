Public Module CoordinatePairs

    Public Class Sticker
        Public Property FaceNumber As Integer
        Public Property StickerNumber As Integer

        Public Sub New()
            FaceNumber = 0
            StickerNumber = 0
        End Sub
        Public Sub New(ByVal x As Integer, ByVal y As Integer)
            Me.FaceNumber = x
            Me.StickerNumber = y
        End Sub
        Public Sub New(ByVal button As Button)
            Me.FaceNumber = Mid(button.Name, 2, 1) - 1 'e.g. a5 -> 4
            Dim stickerLetter As String = Mid(button.Name, 1, 1) 'e.g. a5 -> a
            Me.StickerNumber = Asc(stickerLetter) - 97
        End Sub

        Public Shared Operator =(ByVal sticker1 As Sticker, ByVal sticker2 As Sticker) As Boolean
            Return sticker1.FaceNumber = sticker2.FaceNumber And sticker1.StickerNumber = sticker2.StickerNumber
        End Operator

        Public Shared Operator <>(ByVal sticker1 As Sticker, ByVal sticker2 As Sticker) As Boolean
            Return sticker1.FaceNumber <> sticker2.FaceNumber Or sticker1.StickerNumber <> sticker2.StickerNumber
        End Operator
    End Class

    ' A collection of 3 stickers that make up a corner cubie
    Public Class CornerTriplet

        Private ReadOnly cornerTriples(,) As Sticker = {{New Sticker, New Sticker, New Sticker},
                                                        {New Sticker, New Sticker, New Sticker},
                                                        {New Sticker, New Sticker, New Sticker},
                                                        {New Sticker, New Sticker, New Sticker}}

        Private _corners() As Sticker = {New Sticker, New Sticker, New Sticker}

        Public Property Corners(ByVal index As Integer) As Sticker
            Get
                Return _corners(index)
            End Get
            Set(value As Sticker)
                _corners(index) = value
            End Set
        End Property

        Public Sub New(ByVal stickerOne As Sticker)

            If cornerTriples(0, 2).FaceNumber = 0 Then SetUpCornerTriplets()
            _corners(0) = stickerOne

            Dim pairIndex, argumentCornerIndex As Integer

            ' Gets the index for the triple containing the sticker passed as an argument
            For j = 0 To UBound(cornerTriples, 2)
                For i = 0 To UBound(cornerTriples, 1)
                    If cornerTriples(i, j) <> stickerOne Then Continue For
                    pairIndex = i
                    argumentCornerIndex = j
                Next
            Next

            ' sets stickers 2 and 3
            Dim index As Integer = 1
            For i = 0 To UBound(cornerTriples, 2)
                If i = argumentCornerIndex Then Continue For
                _corners(index) = cornerTriples(pairIndex, i)
                index = 2
            Next

        End Sub

        Private Sub SetUpCornerTriplets()
            For i = 0 To 3
                cornerTriples(i, 0).FaceNumber = 0
                cornerTriples(i, 1).StickerNumber = 0
                cornerTriples(i, 2).StickerNumber = 2
            Next
            cornerTriples(0, 0).StickerNumber = 0 : cornerTriples(0, 1).FaceNumber = 1 : cornerTriples(0, 2).FaceNumber = 2
            cornerTriples(1, 0).StickerNumber = 2 : cornerTriples(1, 1).FaceNumber = 2 : cornerTriples(1, 2).FaceNumber = 3
            cornerTriples(2, 0).StickerNumber = 6 : cornerTriples(2, 1).FaceNumber = 4 : cornerTriples(2, 2).FaceNumber = 1
            cornerTriples(3, 0).StickerNumber = 8 : cornerTriples(3, 1).FaceNumber = 3 : cornerTriples(3, 2).FaceNumber = 4
        End Sub

    End Class


    ' A collection of 2 stickers that make up an edge cubie
    Public Class EdgePair

        Private ReadOnly edgePairs(,) As Sticker = {{New Sticker, New Sticker},
                                                    {New Sticker, New Sticker},
                                                    {New Sticker, New Sticker},
                                                    {New Sticker, New Sticker},
                                                    {New Sticker, New Sticker},
                                                    {New Sticker, New Sticker},
                                                    {New Sticker, New Sticker},
                                                    {New Sticker, New Sticker}}

        Private _edges() As Sticker = {New Sticker, New Sticker}
        Public Property Edges(ByVal index As Integer) As Sticker
            Get
                Return _edges(index)
            End Get
            Set(value As Sticker)
                _edges(index) = value
            End Set
        End Property

        Public Sub New(ByVal stickerOne As Sticker)
            If edgePairs(0, 1).StickerNumber = 0 Then SetUpEdgePairs()
            _edges(0) = stickerOne


            Dim pairIndex, argumentEdgeIndex As Integer

            ' Gets the index for the pair containing the sticker passed as an argument
            For j = 0 To UBound(edgePairs, 2)
                For i = 0 To UBound(edgePairs, 1)
                    If edgePairs(i, j) <> _edges(0) Then Continue For
                    pairIndex = i
                    argumentEdgeIndex = j
                Next
            Next

            ' Sets sticker 2
            For i = 0 To UBound(edgePairs, 2)
                If i = argumentEdgeIndex Then Continue For
                _edges(1) = edgePairs(pairIndex, i)
            Next

        End Sub

        Private Sub SetUpEdgePairs()
            For i = 0 To 3
                edgePairs(i, 0).FaceNumber = 0
                edgePairs(i, 1).StickerNumber = 1
            Next
            For i = 4 To 7
                edgePairs(i, 0).StickerNumber = 3
                edgePairs(i, 1).StickerNumber = 5
            Next
            edgePairs(0, 0).StickerNumber = 1 : edgePairs(0, 1).FaceNumber = 2
            edgePairs(1, 0).StickerNumber = 3 : edgePairs(1, 1).FaceNumber = 1
            edgePairs(2, 0).StickerNumber = 5 : edgePairs(2, 1).FaceNumber = 3
            edgePairs(3, 0).StickerNumber = 7 : edgePairs(3, 1).FaceNumber = 4

            edgePairs(4, 0).FaceNumber = 1 : edgePairs(4, 1).FaceNumber = 2
            edgePairs(5, 0).FaceNumber = 2 : edgePairs(5, 1).FaceNumber = 3
            edgePairs(6, 0).FaceNumber = 3 : edgePairs(6, 1).FaceNumber = 4
            edgePairs(7, 0).FaceNumber = 4 : edgePairs(7, 1).FaceNumber = 1
        End Sub

    End Class

End Module
