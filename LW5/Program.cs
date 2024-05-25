using lab5;

Digital_automaton a = new Digital_automaton();
a.Print_Table();
a.Minimize();
Console.WriteLine(a.min_sdnf_1);
Console.WriteLine(a.min_sdnf_2);
Console.WriteLine(a.min_sdnf_3);