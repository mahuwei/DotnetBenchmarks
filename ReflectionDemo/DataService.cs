namespace ReflectionDemo;

public class DataService {
    private static DataService? _service;
    private readonly DsDemo _dsDemo;

    private DataService() {
        _dsDemo = new DsDemo();
        var row = _dsDemo.Employee.NewEmployeeRow();
        row.Id = Guid.NewGuid().ToString();
        row.Name = "mhw";
        row.Age = 10;
        row.Address = null;
        row.MobileNo = "13934238919";
        row.Memo = null;
        _dsDemo.Employee.AddEmployeeRow(row);
        _dsDemo.Employee.AcceptChanges();
    }

    public DsDemo GetDemo() {
        return _dsDemo;
    }

    public static DataService GetService() {
        return _service ??= new DataService();
    }
}