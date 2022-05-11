Imports System.Data.SqlClient

Module ModuleMain
    Dim cnReturn As SqlConnection
    Dim strConnect As String = "Server=.;Database=PTSekawanIntiplast;User Id=sa;Password=mukhammadfauzi;Trusted_Connection=True"

    Public Function QueryString(ByVal csvDataSet As DataSet, ByVal sqlQuery As String) As DataSet
        cnReturn = New SqlConnection(strConnect)
        Using (cnReturn)
            Dim sqlComm As New SqlCommand
            Dim adapter As New SqlDataAdapter()
            Dim ds As New DataSet()

            sqlComm.Connection = cnReturn
            sqlComm.CommandText = sqlQuery

            cnReturn.Open()
            sqlComm.ExecuteNonQuery()

            adapter = New SqlDataAdapter(sqlComm)
            adapter.Fill(csvDataSet)
            Return csvDataSet
            cnReturn.Close()
        End Using
    End Function

    Public Function StoreProc(ByVal csvDataSet As DataSet, ByVal Code As String, ByVal Idkaryawan As String,
                              ByVal NamaKaryawan As String, ByVal NIK As String, ByVal Alamat As String) As DataSet
        cnReturn = New SqlConnection(strConnect)
        Using (cnReturn)
            Dim sqlComm As New SqlCommand
            Dim adapter As New SqlDataAdapter()

            sqlComm.Connection = cnReturn
            sqlComm.CommandText = "AllSyncProcedure"
            sqlComm.CommandType = CommandType.StoredProcedure

            sqlComm.Parameters.AddWithValue("code", Code)
            sqlComm.Parameters.AddWithValue("idkaryawan", Idkaryawan)
            sqlComm.Parameters.AddWithValue("namakaryawan", NamaKaryawan)
            sqlComm.Parameters.AddWithValue("nik", NIK)
            sqlComm.Parameters.AddWithValue("alamat", Alamat)

            cnReturn.Open()
            sqlComm.ExecuteNonQuery()

            If Code = "GetAll" Then
                adapter = New SqlDataAdapter(sqlComm)
                adapter.Fill(csvDataSet)
                Return csvDataSet
            Else
                Return Nothing
            End If
            cnReturn.Close()
        End Using
    End Function
End Module
