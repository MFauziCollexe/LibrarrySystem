Imports System.ComponentModel
Imports System.Text


Public Class FrmMain
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub btnGetDataIntoDataSet_Click(sender As Object, e As EventArgs) Handles btnGetDataIntoDataSet.Click
        Dim DataSetView As New DataSet()
        QueryString(DataSetView, "Select * from Tkaryawan")
    End Sub

    Private Sub btnCallStoreProcedure_Click(sender As Object, e As EventArgs) Handles btnCallStoreProcedure.Click
        Dim DataSetView As New DataSet()
        StoreProc("AllSyncProcedure", DataSetView, "GetAll", "", "", "", "")

        'If Wanna Insert Data Then Code Like This
        'StoreProc("AllSyncProcedure", DataSetView, "Save", "IT-0001", "Uzzi", "3500325001", "Jl.Pahlawan No 1")
        'If Wanna Delete Data Then Code Like This
        'StoreProc(DataSetView, "Save", "IT-0001", "", "", "")
    End Sub

    Private Sub GetDataIntoGridview_Click(sender As Object, e As EventArgs) Handles GetDataIntoGridview.Click
        Dim DataSetView As New DataSet()
        StoreProc("AllSyncProcedure", DataSetView, "GetAll", "", "", "", "")
        DataGridView1.DataSource = DataSetView.Tables(0)

        'Or we can use Query String
        'QueryString("AllSyncProcedure", DataSetView, "Select * from Tkaryawan")
        'DataGridView1.DataSource = DataSetView.Tables(0)
    End Sub

    Private Sub DataGridView1_CurrentCellChanged(sender As Object, e As EventArgs) Handles DataGridView1.CurrentCellChanged
        Dim iRowIndex As Integer

        For i As Integer = 0 To Me.DataGridView1.SelectedCells.Count - 1
            iRowIndex = Me.DataGridView1.SelectedCells.Item(i).RowIndex
        Next
        txtIdKaryawan.Text = DataGridView1.Rows(iRowIndex).Cells(0).Value.ToString
        txtNamaKaryawan.Text = DataGridView1.Rows(iRowIndex).Cells(1).Value.ToString
        txtNIK.Text = DataGridView1.Rows(iRowIndex).Cells(2).Value.ToString
        txtAlamat.Text = DataGridView1.Rows(iRowIndex).Cells(3).Value.ToString

    End Sub

    Private Sub btnCallDataFromSQLite_Click(sender As Object, e As EventArgs) Handles btnCallDataFromSQLite.Click
        Dim Dt As New DataTable()
        DataGridView1.DataSource = QueryStringSQLite(Dt, "Select * from Tkaryawan")
    End Sub

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        ExportExcel(DataGridView1)
    End Sub

    Private Sub btnImportExcel_Click(sender As Object, e As EventArgs) Handles btnImportExcel.Click
        MessageBox.Show("Sorry This Button Underconstruction")
    End Sub
End Class
