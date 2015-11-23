## CourseSystem 选修课系统

CourseSystem is aim to achieve the course apply which include the server and the client communicate with network. CS 是基于网络的用于课程报名的一个系统，其中包括服务端和客户端。

## CourseServer
CourseServer is act like [Laravel](http://www.laravel.com) that provide a lot of features such as Route Selecting, Middleware Supporting, and the MVC based program design. Although it is not stronger enough as Laravel do, the framework can be used to build a simple server easy that expose the service for the client. CourseServer 模仿 Laravel 框架的思想，实现了基本的路由选择，中间件选择和 MVC 设计模式。虽然没办法更 Laravel 相提并论，但是其简洁的思想可以用于快速构建服务端（所以还是用了 PHP 思想写 .Net（大雾））。

## CourseProvider
As the name of description, CourseProvider provide a serial of APIs that can be easier to used by client communicate with the server and share the code among different clients. CourseProvider 对服务端的请求进行封装，用于在客户端的代码共享。

## CourseClient
There have three client for the end-user: ManagerClient, TeacherClient and StudentClient. Each of client just provide the GUI to the user and access the service via CourseProvider APIs. All of them using WPF to represent. CourseClient 包含了管理员、教师和学生客户端，其使用 CoureProvider 提供的 API 与服务器进行通信，三个客户端均使用 WPF 实现。
