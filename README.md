# WebForm-RequestBinder
簡單的WebForm ModelBinder 

使用方法很簡單 先建立一個想要接收前端Post或Get的基本類別
如:

 class Student
 
{

    public int? StudentID { get; set; }
    public string StudentName { get; set; }
    public int StudentAge { get; set; }
    public double studentAdvScore { get; set; }
    public DateTime? BirthDay { get; set; }
    public HttpPostedFile Photo { get; set; }
    
}

protected void Page_Load(object sender, EventArgs e)

{

    //實体化物件
    Student student = new Student();
    //將實体化後的物件傳入建構式 RequestMethod分為POST和GET 不傳入時預設GET 並將目前的Page.Request物件傳入
    BinderByRequest Model = new BinderByRequest(student, Request, BinderByRequest.RequestMethod.post);
    //執行Binding
    Model.ModelBinding();
    //秀出結果
    Response.Write(student.StudentID + "<br/>" + student.StudentName +
        "<br/>" + student.StudentAge + "<br/>" + student.studentAdvScore + "<br/>" + student.BirthDay
        + "<br/>" + student.Photo.FileName);
        
}

Binding若失敗會回傳代入預設值

若不是支援的型態則會成為null
