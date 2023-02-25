using Microsoft.AspNetCore.Mvc;

namespace FirstApp.Controller{
public class HomeController
{
    [Route("sayhello")]
    public string method1(){
        return "Hello World";
    }
}
}