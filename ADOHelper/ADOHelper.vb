Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient

Public Class QueryField
    Public Property ColumnName As String
    Public Property ColumnOrdinal As Integer
    Public Property ColumnSize As Integer
    Public Property NumericPrecision As Integer
    Public Property NumericScale As Integer
    Public Property IsUnique As Boolean
    Public Property BaseColumnName As String
    Public Property BaseTableName As String
    Public Property DataType As String
    Public Property AllowDBNull As Boolean
    Public Property ProviderType As String
    Public Property IsIdentity As Boolean
    Public Property IsAutoIncrement As Boolean
    Public Property IsRowVersion As Boolean
    Public Property IsLong As Boolean
    Public Property IsReadOnly As Boolean
    Public Property ProviderSpecificDataType As String
    Public Property DataTypeName As String
    Public Property UdtAssemblyQualifiedName As String
    Public Property NewVersionedProviderType As Integer
    Public Property IsColumnSet As String
    Public Property RawProperties As String
    Public Property NonVersionedProviderType As String
End Class

Public Class ADOHelper

    Public Function GetFields(ConnectionString As String, Query As String, ByRef SchemaTable As DataTable) As List(Of QueryField)
        Dim dt As New DataTable
        If (Query.Trim.ToLower.StartsWith("select ")) Then
            SchemaTable = GetQuerySchema(ConnectionString, Query)
        Else
            SchemaTable = GetSPSchema(Query, ConnectionString)
        End If
        Dim strReader As New List(Of QueryField)

        For i As Integer = 0 To SchemaTable.Rows.Count - 1
            Dim qf = New QueryField
            Dim properties As String = String.Empty
            For j = 0 To SchemaTable.Columns.Count - 1
                properties += SchemaTable.Columns(j).ColumnName & Chr(254) & SchemaTable.Rows(i).Item(j).ToString
                If j < SchemaTable.Columns.Count - 1 Then properties += Chr(255)

                If (IsDBNull(SchemaTable.Rows(i).Item(j)) = False) Then
                    Select Case SchemaTable.Columns(j).ColumnName
                        Case "ColumnName"
                            qf.ColumnName = SchemaTable.Rows(i).Item(j)
                        Case "ColumnOrdinal"
                            qf.ColumnOrdinal = SchemaTable.Rows(i).Item(j)
                        Case "ColumnSize"
                            qf.ColumnSize = SchemaTable.Rows(i).Item(j)
                        Case "NumericPrecision"
                            qf.NumericPrecision = SchemaTable.Rows(i).Item(j)
                        Case "NumericScale"
                            qf.NumericScale = SchemaTable.Rows(i).Item(j)
                        Case "IsUnique"
                            qf.IsUnique = SchemaTable.Rows(i).Item(j)
                        Case "BaseColumnName"
                            qf.BaseColumnName = SchemaTable.Rows(i).Item(j)
                        Case "BaseTableName"
                            qf.BaseTableName = SchemaTable.Rows(i).Item(j)
                        Case "DataType"
                            qf.DataType = CType(SchemaTable.Rows(i).Item(j), System.Type).FullName
                        Case "AllowDBNull"
                            qf.AllowDBNull = SchemaTable.Rows(i).Item(j)
                        Case "ProviderType"
                            qf.ProviderType = SchemaTable.Rows(i).Item(j)
                        Case "IsIdentity"
                            qf.IsIdentity = SchemaTable.Rows(i).Item(j)
                        Case "IsAutoIncrement"
                            qf.IsAutoIncrement = SchemaTable.Rows(i).Item(j)
                        Case "IsRowVersion"
                            qf.IsRowVersion = SchemaTable.Rows(i).Item(j)
                        Case "IsLong"
                            qf.IsLong = SchemaTable.Rows(i).Item(j)
                        Case "IsReadOnly"
                            qf.IsReadOnly = SchemaTable.Rows(i).Item(j)
                        Case "ProviderSpecificDataType"
                            qf.ProviderSpecificDataType = CType(SchemaTable.Rows(i).Item(j), System.Type).FullName
                        Case "DataTypeName"
                            qf.DataTypeName = SchemaTable.Rows(i).Item(j)
                        Case "UdtAssemblyQualifiedName"
                            qf.UdtAssemblyQualifiedName = SchemaTable.Rows(i).Item(j)
                        Case "IsColumnSet"
                            qf.IsColumnSet = SchemaTable.Rows(i).Item(j)
                        Case "NonVersionedProviderType"
                            qf.NonVersionedProviderType = SchemaTable.Rows(i).Item(j)
                        Case Else
                    End Select
                End If
            Next
            qf.RawProperties = properties
            strReader.Add(qf)
        Next

        Return strReader
    End Function

    Sub New()
    End Sub

    'Perform the query, extract the strReaders
    Private Function GetQuerySchema(ByVal strconn As String, ByVal strSQL As String) As DataTable
        'Returns a DataTable filled with the strReaders of the query
        'Function returns the count of records in the datatable
        '----- dt (datatable) needs to be empty & no schema defined

        Dim sconQuery As New SqlConnection
        Dim scmdQuery As New SqlCommand
        Dim srdrQuery As SqlDataReader = Nothing
        Dim intRowsCount As Integer = 0
        Dim dtSchema As New Data.DataTable

        Try

            'Open the SQL connnection to the SWO database
            sconQuery.ConnectionString = strconn
            sconQuery.Open()

            'Execute the SQL command against the database & return a strReaderset
            scmdQuery.Connection = sconQuery
            scmdQuery.CommandText = strSQL
            srdrQuery = scmdQuery.ExecuteReader(Data.CommandBehavior.SchemaOnly)

            dtSchema = srdrQuery.GetSchemaTable
        Catch ex As Exception
            Err.Raise(-1000, , "Error = '" & ex.Message & " ': sql = " & strSQL)
        Finally
            If Not IsNothing(srdrQuery) Then
                If Not srdrQuery.IsClosed Then srdrQuery.Close()
            End If
            scmdQuery.Dispose()
            sconQuery.Close()
            sconQuery.Dispose()
        End Try

        Return dtSchema
    End Function

    Public Function GenerateCodeVB(ByRef Columns As List(Of QueryField), ObjectName As String, Optional LinePrefix As String = "    ") As String()
        Dim strResult(Columns.Count * 2 + 20) As String
        Dim lCounter As Integer = Columns.Count + 5
        Dim lColumnPosFix As String

        strResult(0) = String.Format("Public Class {0}", ObjectName)
        strResult(1) = "#Region ""Primitive Properties"""
        For i As Integer = 0 To Columns.Count - 1
            Try
                If String.IsNullOrEmpty(Columns(i).ColumnName) Then
                    Columns(i).ColumnName = "Field" & i.ToString("000")
                End If
                If Char.IsNumber(Columns(i).ColumnName.Substring(0, 1)) Then
                    Columns(i).ColumnName = "_" & Columns(i).ColumnName
                End If

                Dim AllowNull As String = ", null"
                If Columns(i).AllowDBNull = False Then
                    AllowNull = ", Not null"
                    lColumnPosFix = ""
                Else
                    lColumnPosFix = "?"
                End If

                Select Case Columns(i).DataTypeName

                    Case "bigint"
                        strResult(i + 2) = String.Format("{0}Public Property {1} As Long{2} '(bigint{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "binary"
                        strResult(i + 2) = String.Format("{0}Public Property {1}() as Byte{2} '(binary({3}){4})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, Columns(i).ColumnSize, AllowNull)

                    Case "bit"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as Boolean{2} '(bit{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "char"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as String '(char({2}){3})", LinePrefix, Columns(i).ColumnName, Columns(i).ColumnSize, AllowNull)

                    Case "date"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as DateTime{2} '(date{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "datetime"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as DateTime{2} '(datetime{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "datetime2"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as DateTime{2} '(datetime2({3}){4})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, Columns(i).NumericScale, AllowNull)

                    Case "datetimeoffset"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as DateTimeOffset{2} '(datetimeoffset{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "decimal"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as Decimal{2} '(decimal({3},{4}){5})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, Columns(i).NumericPrecision, Columns(i).NumericScale, AllowNull)

                    Case "float"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as Double{2} '(float{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "image"
                        strResult(i + 2) = String.Format("{0}Public Property {1}() as Byte{2} '(image{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "int"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as Integer{2} '(int{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "money"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as Decimal{2} '(money{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "nchar"
                        If Columns(i).IsLong Then
                            strResult(i + 2) = String.Format("{0}Public Property {1} as String '(nchar(max){2})", LinePrefix, Columns(i).ColumnName, AllowNull)
                        Else
                            strResult(i + 2) = String.Format("{0}Public Property {1} as String '(nchar({2}){3})", LinePrefix, Columns(i).ColumnName, Columns(i).ColumnSize, AllowNull)
                        End If

                    Case "ntext"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as String '(ntext{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "nvarchar"
                        If Columns(i).IsLong Then
                            strResult(i + 2) = String.Format("{0}Public Property {1} as String '(nvarchar(max){2})", LinePrefix, Columns(i).ColumnName, AllowNull)
                        Else
                            strResult(i + 2) = String.Format("{0}Public Property {1} as String '(nvarchar({2}){3})", LinePrefix, Columns(i).ColumnName, Columns(i).ColumnSize, AllowNull)
                        End If

                    Case "real"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as Single{2} '(real{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "smalldatetime"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as DateTime{2} '(smalldatetime{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "sql_variant"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as Object{2} '(sql_variant{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "text"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as String '(text{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "time"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as DateTime{2} '(time({3}){4})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, Columns(i).NumericScale, AllowNull)

                    Case "timestamp"
                        strResult(i + 2) = String.Format("{0}Public Property {1}() as Byte{2} '(timestamp{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "tinyint"
                        strResult(i + 2) = String.Format("{0}Public Property {1}() as Byte{2} '(tinyint{3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "uniqueidentifier"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as Guid '(uniqueidentifier{2})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)

                    Case "varbinary"
                        If Columns(i).IsLong Then
                            strResult(i + 2) = String.Format("{0}Public Property {1}() as Byte{2} '(varbinary(max){3})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, AllowNull)
                        Else
                            strResult(i + 2) = String.Format("{0}Public Property {1}() as Byte{2} '(varbinary({3}){4})", LinePrefix, Columns(i).ColumnName, lColumnPosFix, Columns(i).ColumnSize, AllowNull)
                        End If

                    Case "varchar"
                        If Columns(i).IsLong Then
                            strResult(i + 2) = String.Format("{0}Public Property {1} as String '(varchar(max){2})", LinePrefix, Columns(i).ColumnName, AllowNull)
                        Else
                            strResult(i + 2) = String.Format("{0}Public Property {1} as String '(varchar({2}){3})", LinePrefix, Columns(i).ColumnName, Columns(i).ColumnSize, AllowNull)
                        End If

                    Case "xml"
                        strResult(i + 2) = String.Format("{0}Public Property {1} as String '(XML(.){2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case Else 'sql variant
                        Select Case Columns(i).DataType

                            Case "Microsoft.SqlServer.Types.SqlGeography" 'geography
                                strResult(i + 2) = String.Format("{0}Public Property {1} as Microsoft.SqlServer.Types.SqlGeography '({2}{3})", LinePrefix, Columns(i).ColumnName, Columns(i).DataTypeName, AllowNull)

                            Case "Microsoft.SqlServer.Types.SqlHierarchyId" 'heirarchyid
                                strResult(i + 2) = String.Format("{0}Public Property {1} as Microsoft.SqlServer.Types.SqlGeography '({2}{3})", LinePrefix, Columns(i).ColumnName, Columns(i).DataTypeName, AllowNull)

                            Case "Microsoft.SqlServer.Types.SqlGeometry" 'geometry
                                strResult(i + 2) = String.Format("{0}Public Property {1} as Microsoft.SqlServer.Types.SqlGeography '({2}{3})", LinePrefix, Columns(i).ColumnName, Columns(i).DataTypeName, AllowNull)

                        End Select
                End Select
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        Next
        strResult(Columns.Count + 2) = "#End Region"
        strResult(Columns.Count + 3) = ""
        strResult(Columns.Count + 4) = "#Region ""Public Functions"""

        strResult(lCounter) = String.Format("Public Function reader2Item(pReader As System.Data.SqlClient.SqlDataReader) As {0}", ObjectName)
        strResult(lCounter + 1) = vbTab + "With pReader"

        For i As Integer = 0 To Columns.Count - 1
            Try
                Select Case Columns(i).DataTypeName
                    Case "bigint"
                        If Columns(i).AllowDBNull Then
                            strResult(lCounter + i + 2) = vbTab + vbTab + String.Format("if not .IsDBNull (.GetOrdinal(""{0}"")) Then ", Columns(i).BaseColumnName)
                        Else
                            strResult(lCounter + i + 2) = vbTab + vbTab
                        End If
                        strResult(lCounter + i + 2) += String.Format("Me.{0} = .GetInt64(.GetOrdinal(""{1}""))", Columns(i).ColumnName, Columns(i).BaseColumnName)

                    Case "binary"
                        If Columns(i).AllowDBNull Then
                            strResult(lCounter + i + 2) = vbTab + vbTab + String.Format("if not .IsDBNull (.GetOrdinal(""{0}"")) Then ", Columns(i).BaseColumnName)
                        Else
                            strResult(lCounter + i + 2) = vbTab + vbTab
                        End If
                        strResult(lCounter + i + 2) += String.Format("Me.{0} = .GetSqlBinary(.GetOrdinal(""{1}""))", Columns(i).ColumnName, Columns(i).BaseColumnName)

                    Case "bit"
                        If Columns(i).AllowDBNull Then
                            strResult(lCounter + i + 2) = vbTab + vbTab + String.Format("if not .IsDBNull (.GetOrdinal(""{0}"")) Then ", Columns(i).BaseColumnName)
                        Else
                            strResult(lCounter + i + 2) = vbTab + vbTab
                        End If
                        strResult(lCounter + i + 2) += String.Format("Me.{0} = .GetInt16(.GetOrdinal(""{1}""))", Columns(i).ColumnName, Columns(i).BaseColumnName)

                    Case "char", "text", "varchar"
                        strResult(lCounter + i + 2) = vbTab + vbTab + String.Format("if not .IsDBNull (.GetOrdinal(""{0}"")) Then ", Columns(i).BaseColumnName)
                        strResult(lCounter + i + 2) += String.Format("Me.{0} = .GetString(.GetOrdinal(""{1}""))", Columns(i).ColumnName, Columns(i).BaseColumnName)

                    Case "date", "datetime"
                        If Columns(i).AllowDBNull Then
                            strResult(lCounter + i + 2) = vbTab + vbTab + String.Format("if not .IsDBNull (.GetOrdinal(""{0}"")) Then ", Columns(i).BaseColumnName)
                        Else
                            strResult(lCounter + i + 2) = vbTab + vbTab
                        End If
                        strResult(lCounter + i + 2) += String.Format("Me.{0} = .GetDateTime(.GetOrdinal(""{1}""))", Columns(i).ColumnName, Columns(i).BaseColumnName)

                    Case "decimal"
                        If Columns(i).AllowDBNull Then
                            strResult(lCounter + i + 2) = vbTab + vbTab + String.Format("if not .IsDBNull (.GetOrdinal(""{0}"")) Then ", Columns(i).BaseColumnName)
                        Else
                            strResult(lCounter + i + 2) = vbTab + vbTab
                        End If
                        strResult(lCounter + i + 2) += String.Format("Me.{0} = .GetDecimal(.GetOrdinal(""{1}""))", Columns(i).ColumnName, Columns(i).BaseColumnName)

                    Case "float"
                        If Columns(i).AllowDBNull Then
                            strResult(lCounter + i + 2) = vbTab + vbTab + String.Format("if not .IsDBNull (.GetOrdinal(""{0}"")) Then ", Columns(i).BaseColumnName)
                        Else
                            strResult(lCounter + i + 2) = vbTab + vbTab
                        End If
                        strResult(lCounter + i + 2) += String.Format("Me.{0} = .GetFloat(.GetOrdinal(""{1}""))", Columns(i).ColumnName, Columns(i).BaseColumnName)

                    Case "int"
                        If Columns(i).AllowDBNull Then
                            strResult(lCounter + i + 2) = vbTab + vbTab + String.Format("if not .IsDBNull (.GetOrdinal(""{0}"")) Then ", Columns(i).BaseColumnName)
                        Else
                            strResult(lCounter + i + 2) = vbTab + vbTab
                        End If
                        strResult(lCounter + i + 2) += String.Format("Me.{0} = .GetInt32(.GetOrdinal(""{1}""))", Columns(i).ColumnName, Columns(i).BaseColumnName)

                    Case "money"
                        If Columns(i).AllowDBNull Then
                            strResult(lCounter + i + 2) = vbTab + vbTab + String.Format("if not .IsDBNull (.GetOrdinal(""{0}"")) Then ", Columns(i).BaseColumnName)
                        Else
                            strResult(lCounter + i + 2) = vbTab + vbTab
                        End If
                        strResult(lCounter + i + 2) += String.Format("Me.{0} = .GetSqlMoney(.GetOrdinal(""{1}""))", Columns(i).ColumnName, Columns(i).BaseColumnName)

                    Case Else
                        strResult(lCounter + i + 2) = vbTab + vbTab + String.Format("''Columna {0} de tipo {1} no procesada", Columns(i).ColumnName, Columns(i).DataTypeName)

                End Select
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        Next

        strResult(lCounter + Columns.Count + 2) = vbTab + "End With"
        strResult(lCounter + Columns.Count + 3) = vbTab + "Return Me"
        strResult(lCounter + Columns.Count + 4) = "End Function"
        strResult(lCounter + Columns.Count + 5) = "#End Region"
        strResult(lCounter + Columns.Count + 6) = "#Region ""Constructors"""
        strResult(lCounter + Columns.Count + 7) = "public sub new (pReader As System.Data.SqlClient.SqlDataReader)"
        strResult(lCounter + Columns.Count + 8) = vbTab + "mybase.new()"
        strResult(lCounter + Columns.Count + 9) = vbTab + "Me.reader2Item(pReader)"
        strResult(lCounter + Columns.Count + 10) = "End Sub"
        strResult(lCounter + Columns.Count + 11) = "#End Region"
        strResult(lCounter + Columns.Count + 12) = "End Class"

        Return strResult
    End Function

    Public Function GenerateCodeCS(ByRef Columns As List(Of QueryField), ObjectName As String, Optional LinePrefix As String = "    ") As String()
        Dim strReader(Columns.Count + 2) As String
        strReader(0) = String.Format("public class {0} {{", ObjectName)
        For i As Integer = 0 To Columns.Count - 1
            Try

                If Char.IsNumber(Columns(i).ColumnName.Substring(0, 1)) Then
                    Columns(i).ColumnName = "_" & Columns(i).ColumnName
                End If

                Dim AllowNull As String = ", null"
                If Columns(i).AllowDBNull = False Then AllowNull = ", not null"

                Select Case Columns(i).DataTypeName

                    Case "bigint"
                        strReader(i + 1) = String.Format("{0}public long {1} {{ get; set; }} //(bigint{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "binary"
                        strReader(i + 1) = String.Format("{0}public byte[] {1} {{ get; set; }} //(binary({2}){3})", LinePrefix, Columns(i).ColumnName, Columns(i).ColumnSize, AllowNull)

                    Case "bit"
                        strReader(i + 1) = String.Format("{0}public bool {1} {{ get; set; }} //(bit{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "char"
                        strReader(i + 1) = String.Format("{0}public string {1} {{ get; set; }} //(char({2}){3})", LinePrefix, Columns(i).ColumnName, Columns(i).ColumnSize, AllowNull)

                    Case "date"
                        strReader(i + 1) = String.Format("{0}public DateTime {1} {{ get; set; }} //(date{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "datetime"
                        strReader(i + 1) = String.Format("{0}public DateTime {1} {{ get; set; }} //(datetime{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "datetime2"
                        strReader(i + 1) = String.Format("{0}public DateTime {1} {{ get; set; }} //(datetime2({2}){3})", LinePrefix, Columns(i).ColumnName, Columns(i).NumericScale, AllowNull)

                    Case "datetimeoffset"
                        strReader(i + 1) = String.Format("{0}public DateTimeOffset {1} {{ get; set; }} //(datetimeoffset{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "decimal"
                        strReader(i + 1) = String.Format("{0}public decimal {1} {{ get; set; }} //(decimal({2},{3}){4})", LinePrefix, Columns(i).ColumnName, Columns(i).NumericPrecision, Columns(i).NumericScale, AllowNull)

                    Case "float"
                        strReader(i + 1) = String.Format("{0}public double {1} {{ get; set; }} //(float{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "image"
                        strReader(i + 1) = String.Format("{0}public byte[] {1} {{ get; set; }} //(image{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "int"
                        strReader(i + 1) = String.Format("{0}public int {1} {{ get; set; }} //(int{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "money"
                        strReader(i + 1) = String.Format("{0}public decimal {1} {{ get; set; }} //(money{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "nchar"
                        If Columns(i).IsLong Then
                            strReader(i + 1) = String.Format("{0}public string {1} {{ get; set; }} //(nchar(max){2})", LinePrefix, Columns(i).ColumnName, AllowNull)
                        Else
                            strReader(i + 1) = String.Format("{0}public string {1} {{ get; set; }} //(nchar({2}){3})", LinePrefix, Columns(i).ColumnName, Columns(i).ColumnSize, AllowNull)
                        End If

                    Case "ntext"
                        strReader(i + 1) = String.Format("{0}public string {1} {{ get; set; }} //(ntext{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "nvarchar"
                        If Columns(i).IsLong Then
                            strReader(i + 1) = String.Format("{0}public string {1} {{ get; set; }} //(nvarchar(max){2})", LinePrefix, Columns(i).ColumnName, AllowNull)
                        Else
                            strReader(i + 1) = String.Format("{0}public string {1} {{ get; set; }} //(nvarchar({2}){3})", LinePrefix, Columns(i).ColumnName, Columns(i).ColumnSize, AllowNull)
                        End If

                    Case "real"
                        strReader(i + 1) = String.Format("{0}public Single {1} {{ get; set; }} //(real({2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "smalldatetime"
                        strReader(i + 1) = String.Format("{0}public DateTime {1} {{ get; set; }} //(smalldatetime{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "sql_variant"
                        strReader(i + 1) = String.Format("{0}public object {1} {{ get; set; }} //(sql_variant{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "text"
                        strReader(i + 1) = String.Format("{0}public string {1} {{ get; set; }} //(text{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "time"
                        strReader(i + 1) = String.Format("{0}public DateTime {1} {{ get; set; }} //(time({2}){3})", LinePrefix, Columns(i).ColumnName, Columns(i).NumericScale, AllowNull)

                    Case "timestamp"
                        strReader(i + 1) = String.Format("{0}public byte[] {1} {{ get; set; }} //(timestamp{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "tinyint"
                        strReader(i + 1) = String.Format("{0}public byte {1} {{ get; set; }} //(tinyint{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "uniqueidentifier"
                        strReader(i + 1) = String.Format("{0}public Guid {1} {{ get; set; }} //(uniqueidentifier{2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case "varbinary"
                        If Columns(i).IsLong Then
                            strReader(i + 1) = String.Format("{0}public byte[] {1} {{ get; set; }} //(varbinary(max){2})", LinePrefix, Columns(i).ColumnName, Columns(i).ColumnSize, AllowNull)
                        Else
                            strReader(i + 1) = String.Format("{0}public byte[] {1} {{ get; set; }} //(varbinary({2}){3})", LinePrefix, Columns(i).ColumnName, Columns(i).ColumnSize, Columns(i).ColumnSize, AllowNull)
                        End If

                    Case "varchar"
                        If Columns(i).IsLong Then
                            strReader(i + 1) = String.Format("{0}public string {1} {{ get; set; }} //(varchar(max){2})", LinePrefix, Columns(i).ColumnName, AllowNull)
                        Else
                            strReader(i + 1) = String.Format("{0}public string {1} {{ get; set; }} //(varchar({2}){3})", LinePrefix, Columns(i).ColumnName, Columns(i).ColumnSize, AllowNull)
                        End If

                    Case "xml"
                        strReader(i + 1) = String.Format("{0}public string {1} {{ get; set; }} //(XML(.){2})", LinePrefix, Columns(i).ColumnName, AllowNull)

                    Case Else 'sql variant
                        Select Case Columns(i).DataType

                            Case "Microsoft.SqlServer.Types.SqlGeography" 'geography
                                strReader(i + 1) = String.Format("{0}public Microsoft.SqlServer.Types.SqlGeography {1} {{ get; set; }} //({2}{3})", LinePrefix, Columns(i).ColumnName, Columns(i).DataTypeName, AllowNull)

                            Case "Microsoft.SqlServer.Types.SqlHierarchyId" 'heirarchyid
                                strReader(i + 1) = String.Format("{0}public Microsoft.SqlServer.Types.SqlGeography {1} {{ get; set; }} //({2}{3})", LinePrefix, Columns(i).ColumnName, Columns(i).DataTypeName, AllowNull)

                            Case "Microsoft.SqlServer.Types.SqlGeometry" 'geometry
                                strReader(i + 1) = String.Format("{0}public Microsoft.SqlServer.Types.SqlGeography {1} {{ get; set; }} //({2}{3})", LinePrefix, Columns(i).ColumnName, Columns(i).DataTypeName, AllowNull)

                        End Select
                End Select
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        Next
        strReader(Columns.Count + 1) = "}"
        Return strReader
    End Function

    Public Function StringArrayToText(lines() As String, Optional LineDelimiter As String = vbCrLf) As String
        'Determine number of characters needed in strReader string
        Dim charCount = 0
        For Each s In lines
            If Not IsNothing(s) Then
                charCount += s.Length + LineDelimiter.Length
            End If
        Next

        'Preallocate needed string space plus a little extra
        Dim sb = New System.Text.StringBuilder(charCount + lines.Count)
        For i As Integer = 0 To lines.Count - 1
            If Not IsNothing(lines(i)) Then
                sb.Append(lines(i) & LineDelimiter)
            End If
        Next
        Return sb.ToString
    End Function

    'Run a stored procedure with optional parameters and return as datatable of strReaders
    Public Function GetSPSchema(ByVal spName As String, strConn As String) As DataTable

        'Dim the dataset to hold the strReader table
        Dim dtSchema As New DataTable

        'Return if missing important information (SP Name)
        If spName.Trim.Length = 0 Then Return dtSchema

        'Default the connection string to the public class variable if not specified
        If strConn.Length = 0 Then Return dtSchema

        'Create the connection to the database
        Dim sconSP As New SqlClient.SqlConnection
        Dim scmdSP As New SqlClient.SqlCommand
        Dim srdrSP As SqlDataReader = Nothing

        Try
            'Set the connection string on the connection object
            sconSP.ConnectionString = strConn
            sconSP.Open()

            'Set up the SqlCommand object
            scmdSP.CommandText = spName
            scmdSP.Connection = sconSP
            scmdSP.CommandType = CommandType.StoredProcedure

            If spName.Contains("|") Then
                Dim spParms = spName.Split("|")
                scmdSP.CommandText = spParms(0).Trim
                For i = 1 To spParms.Count - 1

                    Dim spFields = spParms(i).Replace("`,", vbTab).Replace("`", "").Split(vbTab)
                    scmdSP.Parameters.Add(New SqlClient.SqlParameter(spFields(0).Trim, spFields(1).Trim))
                Next
            End If

            srdrSP = scmdSP.ExecuteReader(CommandBehavior.SchemaOnly)
            dtSchema = srdrSP.GetSchemaTable
        Catch ex As Exception
            Err.Raise(-1000, , "Error = '" & ex.Message & " ': stored procedure = " & spName)
        Finally
            If Not IsNothing(srdrSP) Then
                If Not srdrSP.IsClosed Then srdrSP.Close()
            End If
            scmdSP.Dispose()
            sconSP.Close()
            sconSP.Dispose()
        End Try

        Return dtSchema
    End Function


End Class

'USE [Chinook]
'GO
'SET ANSI_NULLS ON
'GO
'SET QUOTED_IDENTIFIER ON
'GO
'CREATE PROCEDURE [dbo].[spSelectCustomer]
'AS
'BEGIN
'	SET NOCOUNT ON;
'	SELECT * FROM CUSTOMER
'END
'GO


