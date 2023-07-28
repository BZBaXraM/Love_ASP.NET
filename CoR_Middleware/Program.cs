using CoR_Middleware;

Human human = new("baxram97", "12221312431", "baxram1997007@gmail.com");
var dc = new CheckerDirector();
Console.WriteLine(dc.MakeHumanChecker(human));
