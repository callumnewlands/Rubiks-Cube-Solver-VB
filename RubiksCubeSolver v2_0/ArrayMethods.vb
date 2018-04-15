Imports System.Runtime.CompilerServices
Imports RubiksCubeSolver_v2_0.Helpers

Public Module ArrayExtentions

    <Extension()>
    Public Function Zero(ByRef array() As Integer) As Integer()
        If array.Length < 1 Then Return Nothing

        For i = 0 To UBound(array)
            array(i) = 0
        Next

        Return array
    End Function

    <Extension()>
    Public Sub Append(Of T)(ByRef array() As T, ByVal item As T)
        Try
            ReDim Preserve array(array.Length)
            array(array.Length - 1) = item
        Catch ex As NullReferenceException
            array = {item}
        End Try
    End Sub

    <Extension()>
    Public Function Intersect(ByVal faces1() As FaceColour, ByVal faces2() As FaceColour) As FaceColour()
        If faces1.Length < faces2.Length Then
            Dim tmp() As FaceColour = faces1.Copy()
            faces1 = faces2.Copy()
            faces2 = tmp.Copy()
        End If

        Dim intersection() As FaceColour

        For Each colour In faces1
            If faces2.LinearSearch(colour) <> -1 Then intersection.Append(colour)
        Next

        Return intersection

    End Function

    <Extension()>
    Public Function Extract(Of T As Block)(ByVal blocks() As Block) As T()
        Dim tArray(blocks.Length) As T
        Dim index As Integer = 0
        For Each block In blocks
            If block.GetType = GetType(T) Then
                tArray(index) = block
                index += 1
            End If
        Next
        ReDim Preserve tArray(index - 1)
        Return tArray
    End Function

    <Extension()>
    Public Function Copy(ByVal corners() As Corner) As Corner()
        Dim cornerCopy(0 To corners.Length - 1) As Corner

        For i = 0 To corners.Length - 1
            cornerCopy(i) = New Corner With {
                .Name = corners(i).Name,
                .Rotation = corners(i).Rotation,
                .SecondaryFace = corners(i).SecondaryFace,
                .SecondaryRotation = corners(i).SecondaryRotation,
                .Position = corners(i).Position,
                .Colours = corners(i).Colours
            }
        Next
        Return cornerCopy
    End Function

    <Extension()>
    Public Function Copy(ByVal edges() As Edge) As Edge()
        Dim edgeCopy(0 To edges.Length - 1) As Edge

        For i = 0 To edges.Length - 1
            edgeCopy(i) = New Edge With {
                .Name = edges(i).Name,
                .Rotation = edges(i).Rotation,
                .Position = edges(i).Position,
                .Colours = edges(i).Colours
            }
        Next
        Return edgeCopy
    End Function

    <Extension()>
    Public Function Copy(Of T)(ByVal array(,) As T) As T(,)
        Dim rows As Integer = array.GetLength(0) - 1
        Dim columns As Integer = array.GetLength(1) - 1
        Dim copyArray(rows, columns) As T
        For i = 0 To rows
            For j = 0 To columns
                copyArray(i, j) = array(i, j)
            Next
        Next
        Return copyArray
    End Function

    <Extension()>
    Public Function Copy(Of T)(ByVal array() As T) As T()
        Dim copyArray(array.Length - 1) As T
        For i = 0 To array.Length - 1
            copyArray(i) = array(i)
        Next
        Return copyArray
    End Function

    <Extension()>
    Public Function Copy(Of T)(ByVal queue As Queue(Of T)) As Queue(Of T)
        Dim copyQueue As New Queue(Of T)
        Dim numberOfElements As Integer = queue.Count
        For i = 0 To numberOfElements - 1
            copyQueue.Enqueue(queue.ElementAt(i))
        Next
        Return copyQueue
    End Function

    <Extension()>
    Public Function LinearSearch(ByVal array() As Integer, ByVal value As Integer) As Integer
        If array.Length < 1 Then Return -1

        For i = 0 To UBound(array)
            If array(i) = value Then Return i
        Next

        Return -1
    End Function

    <Extension()>
    Public Function LinearSearch(ByVal array() As FaceColour, ByVal value As FaceColour) As FaceColour
        If array.Length < 1 Then Return -1

        For i = 0 To UBound(array)
            If array(i) = value Then Return i
        Next

        Return -1
    End Function

    <Extension()>
    Public Function AnythingElseInArray(ByVal array() As Integer, ByVal value As Integer) As Boolean
        If array.Length < 1 Then Return True
        For i = 0 To UBound(array)
            If array(i) <> value Then Return True
        Next
        Return False
    End Function

    <Extension()>
    Public Function AnythingElseInArray(ByVal array(,) As Boolean, ByVal value As Boolean) As Boolean
        If array.Length < 1 Then Return True
        For i = 0 To array.GetLength(0) - 1
            For j = 0 To array.GetLength(1) - 1
                If array(i, j) <> value Then Return True
            Next
        Next
        Return False
    End Function

    <Extension()>
    Public Function CountNotNothing(Of T)(ByVal array() As T) As Integer
        Dim count As Integer = 0
        For Each item In array
            If item IsNot Nothing Then
                count += 1
            End If
        Next
        Return count
    End Function

    <Extension()>
    Public Function IsLastElement(Of T)(array As T(), element As T) As Boolean
        Return System.Array.IndexOf(array, element) = array.Length - 1
    End Function

    ''' <summary>
    ''' Cycles an array round so so that the element at newStartIndex is now at index 0
    ''' </summary>
    ''' <param name="indexOfNewStart">0 based index of the item to be at the start of the returned array</param>
    <Extension()>
    Public Function Rotate(Of T)(ByVal array() As T, ByVal indexOfNewStart As Integer) As T()

        Dim rotatedArray(array.Length - 1) As T
        For i = 0 To array.Length - 1
            rotatedArray(i) = array((indexOfNewStart + i) Mod (array.Length))
        Next
        Return rotatedArray

    End Function

End Module

