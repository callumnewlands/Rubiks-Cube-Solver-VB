Imports OpenTK.Graphics.OpenGL
Imports System.IO

Public Class ShaderProgram

    Private program As Integer

    Public ReadOnly Property Handle() As Integer
        Get
            Return program
        End Get
    End Property

    Public Property VBO As Integer
    Public Property VAO As Integer
    Public Property EBO As Integer

    Sub New()

        Dim versionNo As Integer = CInt(CStr(GL.GetString(StringName.Version)(0) + GL.GetString(StringName.Version)(2)))
        Dim versionString As String = "330"

        If versionNo >= 33 Then
            versionString = "330"
        ElseIf versionNo >= 21 Then
            versionString = "120"
        Else
            MsgBox("3D output is not supported by your graphics library")
            program = -1
            versionString = "-1"
            Return
        End If

        Dim vertexShader As Integer
        vertexShader = GL.CreateShader(ShaderType.VertexShader)
        GL.ShaderSource(vertexShader, "#version " + versionString + My.Resources.vertexShader)
        GL.CompileShader(vertexShader)
        Console.WriteLine("Vertex Shader Success:" & GL.GetShaderInfoLog(vertexShader).ToString())

        Dim fragmentShader As Integer
        fragmentShader = GL.CreateShader(ShaderType.FragmentShader)
        GL.ShaderSource(fragmentShader, "#version " + versionString + My.Resources.fragmentShader)
        GL.CompileShader(fragmentShader)
        Console.WriteLine("Fragment Shader Success:" & GL.GetShaderInfoLog(fragmentShader).ToString())

        program = GL.CreateProgram()
        GL.AttachShader(program, vertexShader)
        GL.AttachShader(program, fragmentShader)
        GL.LinkProgram(program)
        Console.WriteLine("Shader Program Success:" & GL.GetProgramInfoLog(program).ToString())

        GL.DeleteShader(vertexShader)
        GL.DeleteShader(fragmentShader)

    End Sub

    Public Sub Use()
        GL.UseProgram(program)
    End Sub


    Public Sub Dispose()
        If Handle = -1 Then Return
        GL.DeleteVertexArray(VAO)
        GL.DeleteBuffer(VBO)
        GL.DeleteBuffer(EBO)
    End Sub

End Class
