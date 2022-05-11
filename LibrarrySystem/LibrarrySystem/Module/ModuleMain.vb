Imports System.Data.SqlClient
Imports System.Data.SQLite
Imports Microsoft.Office.Interop

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

    Public Function StoreProc(ByVal NameStoreProcedure As String, ByVal csvDataSet As DataSet, ByVal Code As String, ByVal Idkaryawan As String,
                              ByVal NamaKaryawan As String, ByVal NIK As String, ByVal Alamat As String) As DataSet
        cnReturn = New SqlConnection(strConnect)
        Using (cnReturn)
            Dim sqlComm As New SqlCommand
            Dim adapter As New SqlDataAdapter()

            sqlComm.Connection = cnReturn
            sqlComm.CommandText = NameStoreProcedure
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

    Public Function QueryStringSQLite(ByVal csvDataSet As DataTable, ByVal sqlQuery As String) As DataTable
        Dim mydata As New DataTable
        Dim FILEPATH As String = Application.StartupPath
        Dim myconnection As New SQLiteConnection("Data Source=" & FILEPATH & "\Sqlite.db")
        Dim mycommand, reader
        myconnection.Open()
        mycommand = New SQLiteCommand(myconnection)
        mycommand.CommandText = sqlQuery
        reader = mycommand.ExecuteReader()
        mydata.Load(reader)
        csvDataSet = mydata
        reader.Close()
        myconnection.Close()
        Return csvDataSet
    End Function

    Sub ExportExcel(ByVal obj As Object)
        Dim rowsTotal, colsTotal As Short
        Dim I, j, iC As Short
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim xlApp As New Excel.Application
        Try
            Dim excelBook As Excel.Workbook = xlApp.Workbooks.Add
            Dim excelWorksheet As Excel.Worksheet = CType(excelBook.Worksheets(1), Excel.Worksheet)
            xlApp.Visible = True

            rowsTotal = obj.RowCount
            colsTotal = obj.Columns.Count - 1
            With excelWorksheet
                .Cells.Select()
                .Cells.Delete()
                For iC = 0 To colsTotal
                    .Cells(1, iC + 1).Value = obj.Columns(iC).HeaderText
                Next
                For I = 0 To rowsTotal - 1
                    For j = 0 To colsTotal
                        .Cells(I + 2, j + 1).value = obj.Rows(I).Cells(j).Value
                    Next j
                Next I
                .Rows("1:1").Font.FontStyle = "Bold"
                .Rows("1:1").Font.Size = 12

                .Cells.Columns.AutoFit()
                .Cells.Select()
                .Cells.EntireColumn.AutoFit()
                .Cells(1, 1).Select()
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            'RELEASE ALLOACTED RESOURCES
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            xlApp = Nothing
        End Try
    End Sub
End Module
