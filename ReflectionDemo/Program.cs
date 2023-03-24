using System.Data;
using System.Globalization;
using ReflectionDemo;

Console.WriteLine("反射测试!");

var dataService = DataService.GetService();
var dsDemo = dataService.GetDemo();
var employeeRow = dsDemo.Employee.First();
var employeeRowType = employeeRow.GetType();
GetDataRowValue(employeeRowType, employeeRow);
SetDataRowValue(employeeRowType, employeeRow);

void SetDataRowValue(Type type, DsDemo.EmployeeRow row) {
    const string propertyMemo = "Memo";
    var propertyInfo = type.GetProperty(propertyMemo);
    if(propertyInfo == null){
        Console.WriteLine($" 未找到属性：{propertyMemo}。");
        return;
    }

    propertyInfo.SetValue(row, DateTime.Now.ToString(CultureInfo.InvariantCulture));
}

Console.WriteLine(" 设置属性值后...");
GetDataRowValue(employeeRowType, employeeRow);

void GetDataRowValue(Type type, DsDemo.EmployeeRow row) {
    foreach(DataColumn column in dsDemo.Employee.Columns)
        try{
            if(row.IsNull(column) == false){
                var value = type.GetProperty(column.ColumnName)?.GetValue(row, null);
                Console.WriteLine(
                    $"GetProperty(..).GetValue(..); ColumnName: {column.ColumnName}; Value:{value}; DataType:{column.DataType.FullName}");
            }
            else{
                Console.WriteLine(
                    $"GetProperty(..).GetValue(..); ColumnName: {column.ColumnName}; Value: null; DataType:{column.DataType.Name}");
            }
        }
        catch(Exception e){
            Console.WriteLine($"{column.ColumnName} GetProperty(..).GetValue(..) 出错。");
            Console.WriteLine(e);
        }
}