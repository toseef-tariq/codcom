#include <iostream>
#include <fstream>
#include<map>
#include<iomanip>
#include <cstdlib>
#include<conio.h>
#include <sstream>
#include <regex> 
#include <cstring>
using namespace std;
string abc [100];
string xyz [100];
static int counter=0,fnd=0;
void lexer (string token){
	string str=token;
	if(!token.find("\"", 0))
	{
		abc[counter]=token;
		xyz[counter]="Message";
		counter++;
	}
	else
	{
		regex rgx("\\s+");
	sregex_token_iterator iter(str.begin(),str.end(), rgx,-1);
	sregex_token_iterator end;
	for ( ; iter != end; ++iter)
	{
		token=*iter;
		int number =atoi (token.c_str());
		if(number>0)
		{
			abc[counter]=token;
			xyz[counter]="Number";
			counter++;
		}
		else
		{
		map<string,string> map1;     
		map1[";"]="Semicolon";map1["if"]="KEYWORD_if";map1["else"]="KEYWORD_else";map1["<="]="grt_eql";map1[">="]="sml_eql";map1["_abc"]="Identfier";
		map1["cout"]="KEYWORD_cout";map1["cin"]="KEYWORD_cin";map1["int"]="KEYWORD_int";map1["return"]="KEYWORD_return";map1["("]="pren_open";map1[")"]="pren_close";map1["<<"]="brk_angl";
		abc[counter]=token; //cout<<token<<"\t"<<map1[token]<<"\n";
		xyz[counter]=map1[token];
		counter++;
		}
	}
	}
}
int main()
{
    string filename="D:\program.txt";
    ifstream file(filename.c_str());
	stringstream buffer;
	buffer << file.rdbuf();
    string str = buffer.str();
	regex rgx("(;)|(cin)|(cout)|(<<)|(>>)|(<=)|(>=)|(=)|(&&)|(\\()|(\\))|(\\})|(\\{)");
	sregex_token_iterator iter(str.begin(),str.end(), rgx,-1);
	sregex_token_iterator iters(str.begin(),str.end(), rgx,0);
	sregex_token_iterator end;
	for ( ; iters != end && iter != end; ++iters)
	{
		string token=*iter;	
		lexer (token);  
		token=*iters;
	    lexer(token);
	    ++iter;
	}
	cout<<setw(30)<<left<<"Values"<<setw(30)<<left<<"Description"<<setw(30)<<left<<"Refrence";
	for (int i=0;i<=counter-1;i++)
	{
		for(int j=i+1;j<counter;j++)
		{
			if(abc[i]==abc[j])
			abc[j]="0";
		}
		if(abc[i]!="0")
		cout<<endl<<setw(30)<<left<<abc[i]<<setw(30)<<left<<xyz[i]<<setw(30)<<left<<i;
	}
	getch();
    return 0;
}