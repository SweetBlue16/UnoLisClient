¿
ÜC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Utilities\ProfileDataMapper.cs
	namespace

 	
UnoLisClient


 
.

 
UI

 
.

 
	Utilities

 #
{ 
public 

static 
class 
ProfileDataMapper )
{ 
public 
static 
ClientProfileData '
ToClientModel( 5
(5 6
this6 :!
UnoLisServerReference; P
.P Q
ProfileViewQ \
.\ ]
ProfileData] h
datai m
)m n
{ 	
if 
( 
data 
== 
null 
) 
{ 
return 
null 
; 
} 
return 
new 
ClientProfileData (
{ 
Nickname 
= 
data 
.  
Nickname  (
,( )
FullName 
= 
data 
.  
FullName  (
,( )
Email 
= 
data 
. 
Email "
," #
FacebookUrl 
= 
data "
." #
FacebookUrl# .
,. /
InstagramUrl 
= 
data #
.# $
InstagramUrl$ 0
,0 1
	TikTokUrl 
= 
data  
.  !
	TikTokUrl! *
,* +
MatchesPlayed 
= 
data  $
.$ %
MatchesPlayed% 2
,2 3
Wins 
= 
data 
. 
Wins  
,  !
Losses 
= 
data 
. 
Losses $
,$ %
ExperiencePoints    
=  ! "
data  # '
.  ' (
ExperiencePoints  ( 8
}!! 
;!! 
}"" 	
public$$ 
static$$ !
UnoLisServerReference$$ +
.$$+ ,
ProfileEdit$$, 7
.$$7 8
ProfileData$$8 C!
ToProfileEditContract$$D Y
($$Y Z
this$$Z ^
ClientProfileData$$_ p
data$$q u
)$$u v
{%% 	
if&& 
(&& 
data&& 
==&& 
null&& 
)&& 
{'' 
return(( 
null(( 
;(( 
})) 
return++ 
new++ !
UnoLisServerReference++ ,
.++, -
ProfileEdit++- 8
.++8 9
ProfileData++9 D
{,, 
Nickname-- 
=-- 
data-- 
.--  
Nickname--  (
,--( )
FullName.. 
=.. 
data.. 
...  
FullName..  (
,..( )
Email// 
=// 
data// 
.// 
Email// "
,//" #
FacebookUrl00 
=00 
data00 "
.00" #
FacebookUrl00# .
,00. /
InstagramUrl11 
=11 
data11 #
.11# $
InstagramUrl11$ 0
,110 1
	TikTokUrl22 
=22 
data22  
.22  !
	TikTokUrl22! *
,22* +
Password33 
=33 
data33 
.33  
Password33  (
}44 
;44 
}55 	
}77 
}88  
ÇC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Properties\AssemblyInfo.cs
[

 
assembly

 	
:

	 

AssemblyTitle

 
(

 
$str

 *
)

* +
]

+ ,
[ 
assembly 	
:	 

AssemblyDescription 
( 
$str !
)! "
]" #
[ 
assembly 	
:	 
!
AssemblyConfiguration  
(  !
$str! #
)# $
]$ %
[ 
assembly 	
:	 

AssemblyCompany 
( 
$str 
) 
] 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str ,
), -
]- .
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str 0
)0 1
]1 2
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
["" 
assembly"" 	
:""	 

	ThemeInfo"" 
("" &
ResourceDictionaryLocation## 
.## 
None## #
,### $&
ResourceDictionaryLocation&& 
.&& 
SourceAssembly&& -
))) 
])) 
[33 
assembly33 	
:33	 

AssemblyVersion33 
(33 
$str33 $
)33$ %
]33% &
[44 
assembly44 	
:44	 

AssemblyFileVersion44 
(44 
$str44 (
)44( )
]44) *Ë

éC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\PopUpWindows\SimplePopUpWindow.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
PopUpWindows &
{ 
public 

partial 
class 
SimplePopUpWindow *
:+ ,
Window- 3
{ 
public 
SimplePopUpWindow  
(  !
string! '
title( -
,- .
string/ 5
message6 =
)= >
{ 	
InitializeComponent 
(  
)  !
;! "

TitleLabel 
. 
Content 
=  
title! &
;& '
MessageTextBlock 
. 
Text !
=" #
message$ +
;+ ,
} 	
private 
void 
ClickOkButton "
(" #
object# )
sender* 0
,0 1
RoutedEventArgs2 A
eB C
)C D
{ 	
SoundManager   
.   
	PlayClick   "
(  " #
)  # $
;  $ %
this!! 
.!! 
Close!! 
(!! 
)!! 
;!! 
}"" 	
}## 
}$$ ≥
êC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\PopUpWindows\QuestionPopUpWindow.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
PopUpWindows &
{ 
public 

partial 
class 
QuestionPopUpWindow ,
:- .
Window/ 5
{ 
public 
QuestionPopUpWindow "
(" #
string# )
title* /
,/ 0
string1 7
message8 ?
)? @
{ 	
InitializeComponent 
(  
)  !
;! "

TitleLabel 
. 
Content 
=  
title! &
;& '
MessageTextBlock 
. 
Text !
=" #
message$ +
;+ ,
} 	
private 
void 
ClickNoButton "
(" #
object# )
sender* 0
,0 1
RoutedEventArgs2 A
eB C
)C D
{ 	
SoundManager   
.   
	PlayClick   "
(  " #
)  # $
;  $ %
this!! 
.!! 
DialogResult!! 
=!! 
false!!  %
;!!% &
}"" 	
private$$ 
void$$ 
ClickYesButton$$ #
($$# $
object$$$ *
sender$$+ 1
,$$1 2
RoutedEventArgs$$3 B
e$$C D
)$$D E
{%% 	
SoundManager&& 
.&& 
	PlayClick&& "
(&&" #
)&&# $
;&&$ %
this'' 
.'' 
DialogResult'' 
='' 
true''  $
;''$ %
}(( 	
})) 
}** ∫
çC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\PopUpWindows\InputPopUpWindow.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
PopUpWindows &
{ 
public 

partial 
class 
InputPopUpWindow )
:* +
Window, 2
{ 
public 
string 
	UserInput 
{  !
get" %
;% &
private' .
set/ 2
;2 3
}4 5
public 
InputPopUpWindow 
(  
string  &
title' ,
,, -
string. 4
message5 <
,< =
string> D
	watermarkE N
)N O
{ 	
InitializeComponent 
(  
)  !
;! "

TitleLabel 
. 
Content 
=  
title! &
;& '
MessageTextBlock 
. 
Text !
=" #
message$ +
;+ ,
InputTextBox 
. 
Tag 
= 
	watermark (
;( )
InputTextBox   
.   
Focus   
(   
)    
;    !
}!! 	
private## 
void## 
ClickOkButton## "
(##" #
object### )
sender##* 0
,##0 1
RoutedEventArgs##2 A
e##B C
)##C D
{$$ 	
SoundManager%% 
.%% 
	PlayClick%% "
(%%" #
)%%# $
;%%$ %
this&& 
.&& 
	UserInput&& 
=&& 
InputTextBox&& )
.&&) *
Text&&* .
.&&. /
Trim&&/ 3
(&&3 4
)&&4 5
;&&5 6
this'' 
.'' 
DialogResult'' 
='' 
true''  $
;''$ %
}(( 	
private** 
void** 
ClickCancelButton** &
(**& '
object**' -
sender**. 4
,**4 5
RoutedEventArgs**6 E
e**F G
)**G H
{++ 	
SoundManager,, 
.,, 
	PlayClick,, "
(,," #
),,# $
;,,$ %
this-- 
.-- 
DialogResult-- 
=-- 
false--  %
;--% &
}.. 	
private00 
void00 #
InputTextBoxTextChanged00 ,
(00, -
object00- 3
sender004 :
,00: ; 
TextChangedEventArgs00< P
e00Q R
)00R S
{11 	
if22 
(22 
OkButton22 
!=22 
null22  
)22  !
{33 
OkButton44 
.44 
	IsEnabled44 "
=44# $
!44% &
string44& ,
.44, -
IsNullOrWhiteSpace44- ?
(44? @
InputTextBox44@ L
.44L M
Text44M Q
)44Q R
;44R S
}55 
}66 	
}77 
}88  y
ÇC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Utilities\UserValidator.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 

Validators $
{ 
public 

static 
class 
UserValidator %
{ 
private 
const 
int 
MinNicknameLength +
=, -
$num. /
;/ 0
private 
const 
int 
MaxNicknameLength +
=, -
$num. 0
;0 1
private 
const 
int 
MaxFullnameLength +
=, -
$num. 1
;1 2
private 
const 
int 
MinPasswordLength +
=, -
$num. /
;/ 0
private 
const 
int 
MaxPasswordLength +
=, -
$num. 0
;0 1
private 
static 
readonly 
Regex  %
_nicknameRegex& 4
=5 6
new7 :
Regex; @
(@ A
$strA S
)S T
;T U
private 
static 
readonly 
Regex  % 
_strongPasswordRegex& :
=; <
new= @
RegexA F
(F G
$str	G ñ
)
ñ ó
;
ó ò
public 
static 
List 
< 
string !
>! " 
ValidateRegistration# 7
(7 8
RegistrationData8 H
registrationDataI Y
,Y Z
string[ a
rewritedPasswordb r
)r s
{ 	
var 
errors 
= 
new 
List !
<! "
string" (
>( )
() *
)* +
;+ ,
if 
( 
string 
. 
IsNullOrWhiteSpace )
() *
registrationData* :
.: ;
Nickname; C
)C D
)D E
{   
errors!! 
.!! 
Add!! 
(!! 
ErrorMessages!! (
.!!( )%
NicknameEmptyMessageLabel!!) B
)!!B C
;!!C D
}"" 
else## 
{$$ 
if%% 
(%% 
!%% 
_nicknameRegex%% #
.%%# $
IsMatch%%$ +
(%%+ ,
registrationData%%, <
.%%< =
Nickname%%= E
)%%E F
)%%F G
{&& 
errors'' 
.'' 
Add'' 
('' 
ErrorMessages'' ,
.'', -&
NicknameFormatMessageLabel''- G
)''G H
;''H I
}(( 
if)) 
()) 
registrationData)) $
.))$ %
Nickname))% -
.))- .
Length)). 4
<))5 6
MinNicknameLength))7 H
||))I K
registrationData))L \
.))\ ]
Nickname))] e
.))e f
Length))f l
>))m n
MaxNicknameLength	))o Ä
)
))Ä Å
{** 
errors++ 
.++ 
Add++ 
(++ 
string++ %
.++% &
Format++& ,
(++, -
ErrorMessages++- :
.++: ;&
NicknameLengthMessageLabel++; U
,++U V
MinNicknameLength++W h
,++h i
MaxNicknameLength++j {
)++{ |
)++| }
;++} ~
},, 
}-- 
if// 
(// 
string// 
.// 
IsNullOrWhiteSpace// )
(//) *
registrationData//* :
.//: ;
FullName//; C
)//C D
)//D E
{00 
errors11 
.11 
Add11 
(11 
ErrorMessages11 (
.11( )%
FullNameEmptyMessageLabel11) B
)11B C
;11C D
}22 
else33 
if33 
(33 
registrationData33 %
.33% &
FullName33& .
.33. /
Length33/ 5
>336 7
MaxFullnameLength338 I
)33I J
{44 
errors55 
.55 
Add55 
(55 
ErrorMessages55 (
.55( )&
FullNameLengthMessageLabel55) C
)55C D
;55D E
}66 
errors88 
.88 
AddRange88 
(88 
ValidateEmail88 )
(88) *
registrationData88* :
.88: ;
Email88; @
)88@ A
)88A B
;88B C
if:: 
(:: 
string:: 
.:: 
IsNullOrWhiteSpace:: )
(::) *
registrationData::* :
.::: ;
Password::; C
)::C D
)::D E
{;; 
errors<< 
.<< 
Add<< 
(<< 
ErrorMessages<< (
.<<( )%
PasswordEmptyMessageLabel<<) B
)<<B C
;<<C D
}== 
else>> 
if>> 
(>> 
!>>  
_strongPasswordRegex>> *
.>>* +
IsMatch>>+ 2
(>>2 3
registrationData>>3 C
.>>C D
Password>>D L
)>>L M
)>>M N
{?? 
errors@@ 
.@@ 
Add@@ 
(@@ 
ErrorMessages@@ (
.@@( )$
WeakPasswordMessageLabel@@) A
)@@A B
;@@B C
}AA 
ifBB 
(BB 
!BB 
registrationDataBB !
.BB! "
PasswordBB" *
.BB* +
EqualsBB+ 1
(BB1 2
rewritedPasswordBB2 B
)BBB C
)BBC D
{CC 
errorsDD 
.DD 
AddDD 
(DD 
ErrorMessagesDD (
.DD( )"
PasswordDontMatchLabelDD) ?
)DD? @
;DD@ A
}EE 
returnGG 
errorsGG 
;GG 
}HH 	
publicJJ 
staticJJ 
ListJJ 
<JJ 
stringJJ !
>JJ! "
ValidateLoginJJ# 0
(JJ0 1
AuthCredentialsJJ1 @
credentialsJJA L
)JJL M
{KK 	
varLL 
errorsLL 
=LL 
newLL 
ListLL !
<LL! "
stringLL" (
>LL( )
(LL) *
)LL* +
;LL+ ,
ifNN 
(NN 
stringNN 
.NN 
IsNullOrWhiteSpaceNN )
(NN) *
credentialsNN* 5
.NN5 6
NicknameNN6 >
)NN> ?
)NN? @
{OO 
errorsPP 
.PP 
AddPP 
(PP 
ErrorMessagesPP (
.PP( )%
NicknameEmptyMessageLabelPP) B
)PPB C
;PPC D
}QQ 
ifRR 
(RR 
stringRR 
.RR 
IsNullOrWhiteSpaceRR )
(RR) *
credentialsRR* 5
.RR5 6
PasswordRR6 >
)RR> ?
)RR? @
{SS 
errorsTT 
.TT 
AddTT 
(TT 
ErrorMessagesTT (
.TT( )%
PasswordEmptyMessageLabelTT) B
)TTB C
;TTC D
}UU 
returnVV 
errorsVV 
;VV 
}WW 	
publicYY 
staticYY 
ListYY 
<YY 
stringYY !
>YY! "!
ValidateProfileUpdateYY# 8
(YY8 9
ProfileDataYY9 D
profileDataYYE P
)YYP Q
{ZZ 	
var[[ 
errors[[ 
=[[ 
new[[ 
List[[ !
<[[! "
string[[" (
>[[( )
([[) *
)[[* +
;[[+ ,
if]] 
(]] 
string]] 
.]] 
IsNullOrWhiteSpace]] )
(]]) *
profileData]]* 5
.]]5 6
FullName]]6 >
)]]> ?
)]]? @
{^^ 
errors__ 
.__ 
Add__ 
(__ 
ErrorMessages__ (
.__( )%
FullNameEmptyMessageLabel__) B
)__B C
;__C D
}`` 
elseaa 
ifaa 
(aa 
profileDataaa  
.aa  !
FullNameaa! )
.aa) *
Lengthaa* 0
>aa1 2
MaxFullnameLengthaa3 D
)aaD E
{bb 
errorscc 
.cc 
Addcc 
(cc 
stringcc !
.cc! "
Formatcc" (
(cc( )
ErrorMessagescc) 6
.cc6 7&
FullNameLengthMessageLabelcc7 Q
,ccQ R
MaxFullnameLengthccS d
)ccd e
)cce f
;ccf g
}dd 
errorsff 
.ff 
AddRangeff 
(ff 
ValidateEmailff )
(ff) *
profileDataff* 5
.ff5 6
Emailff6 ;
)ff; <
)ff< =
;ff= >
ifhh 
(hh 
!hh 
stringhh 
.hh 
IsNullOrWhiteSpacehh *
(hh* +
profileDatahh+ 6
.hh6 7
Passwordhh7 ?
)hh? @
)hh@ A
{ii 
ifjj 
(jj 
!jj  
_strongPasswordRegexjj )
.jj) *
IsMatchjj* 1
(jj1 2
profileDatajj2 =
.jj= >
Passwordjj> F
)jjF G
)jjG H
{kk 
errorsll 
.ll 
Addll 
(ll 
ErrorMessagesll ,
.ll, -$
WeakPasswordMessageLabelll- E
)llE F
;llF G
}mm 
elsenn 
ifnn 
(nn 
profileDatann $
.nn$ %
Passwordnn% -
.nn- .
Lengthnn. 4
<nn5 6
MinPasswordLengthnn7 H
||nnI K
profileDatannL W
.nnW X
PasswordnnX `
.nn` a
Lengthnna g
>nnh i
MaxPasswordLengthnnj {
)nn{ |
{oo 
errorspp 
.pp 
Addpp 
(pp 
stringpp %
.pp% &
Formatpp& ,
(pp, -
ErrorMessagespp- :
.pp: ;&
PasswordLengthMessageLabelpp; U
,ppU V
MinPasswordLengthppW h
,pph i
MaxPasswordLengthppj {
)pp{ |
)pp| }
;pp} ~
}qq 
}rr 
errorstt 
.tt 
AddRangett 
(tt #
ValidateSocialMediaLinktt 3
(tt3 4
profileDatatt4 ?
.tt? @
FacebookUrltt@ K
,ttK L
$strttM [
,tt[ \
$strtt] g
)ttg h
)tth i
;tti j
errorsuu 
.uu 
AddRangeuu 
(uu #
ValidateSocialMediaLinkuu 3
(uu3 4
profileDatauu4 ?
.uu? @
InstagramUrluu@ L
,uuL M
$struuN ]
,uu] ^
$struu_ j
)uuj k
)uuk l
;uul m
errorsvv 
.vv 
AddRangevv 
(vv #
ValidateSocialMediaLinkvv 3
(vv3 4
profileDatavv4 ?
.vv? @
	TikTokUrlvv@ I
,vvI J
$strvvK W
,vvW X
$strvvY a
)vva b
)vvb c
;vvc d
returnxx 
errorsxx 
;xx 
}yy 	
private{{ 
static{{ 
List{{ 
<{{ 
string{{ "
>{{" #
ValidateEmail{{$ 1
({{1 2
string{{2 8
email{{9 >
){{> ?
{|| 	
var}} 
errors}} 
=}} 
new}} 
List}} !
<}}! "
string}}" (
>}}( )
(}}) *
)}}* +
;}}+ ,
if 
( 
string 
. 
IsNullOrWhiteSpace )
() *
email* /
)/ 0
)0 1
{
ÄÄ 
errors
ÅÅ 
.
ÅÅ 
Add
ÅÅ 
(
ÅÅ 
ErrorMessages
ÅÅ (
.
ÅÅ( )$
EmailEmptyMessageLabel
ÅÅ) ?
)
ÅÅ? @
;
ÅÅ@ A
return
ÇÇ 
errors
ÇÇ 
;
ÇÇ 
}
ÉÉ 
try
ÖÖ 
{
ÜÜ 
var
áá 
mailAddress
áá 
=
áá  !
new
áá" %
MailAddress
áá& 1
(
áá1 2
email
áá2 7
)
áá7 8
;
áá8 9
}
àà 
catch
ââ 
(
ââ 
FormatException
ââ "
)
ââ" #
{
ää 
errors
ãã 
.
ãã 
Add
ãã 
(
ãã 
ErrorMessages
ãã (
.
ãã( )%
EmailFormatMessageLabel
ãã) @
)
ãã@ A
;
ããA B
}
åå 
return
çç 
errors
çç 
;
çç 
}
éé 	
private
êê 
static
êê 
List
êê 
<
êê 
string
êê "
>
êê" #%
ValidateSocialMediaLink
êê$ ;
(
êê; <
string
êê< B
url
êêC F
,
êêF G
string
êêH N
expectedDomain
êêO ]
,
êê] ^
string
êê_ e
platformName
êêf r
)
êêr s
{
ëë 	
var
íí 
errors
íí 
=
íí 
new
íí 
List
íí !
<
íí! "
string
íí" (
>
íí( )
(
íí) *
)
íí* +
;
íí+ ,
if
îî 
(
îî 
string
îî 
.
îî  
IsNullOrWhiteSpace
îî )
(
îî) *
url
îî* -
)
îî- .
)
îî. /
{
ïï 
return
ññ 
errors
ññ 
;
ññ 
}
óó 
if
ôô 
(
ôô 
!
ôô 
Uri
ôô 
.
ôô 
	TryCreate
ôô 
(
ôô 
url
ôô "
,
ôô" #
UriKind
ôô$ +
.
ôô+ ,
Absolute
ôô, 4
,
ôô4 5
out
ôô6 9
Uri
ôô: =
	uriResult
ôô> G
)
ôôG H
||
ôôI K
(
ôôL M
	uriResult
ôôM V
.
ôôV W
Scheme
ôôW ]
!=
ôô^ `
Uri
ôôa d
.
ôôd e
UriSchemeHttp
ôôe r
&&
ôôs u
	uriResult
ôôv 
.ôô Ä
SchemeôôÄ Ü
!=ôôá â
Uriôôä ç
.ôôç é
UriSchemeHttpsôôé ú
)ôôú ù
)ôôù û
{
öö 
errors
õõ 
.
õõ 
Add
õõ 
(
õõ 
string
õõ !
.
õõ! "
Format
õõ" (
(
õõ( )
ErrorMessages
õõ) 6
.
õõ6 7*
SocialLinkFormatMessageLabel
õõ7 S
,
õõS T
platformName
õõU a
)
õõa b
)
õõb c
;
õõc d
}
úú 
else
ùù 
if
ùù 
(
ùù 
!
ùù 
	uriResult
ùù 
.
ùù  
Host
ùù  $
.
ùù$ %
Contains
ùù% -
(
ùù- .
expectedDomain
ùù. <
)
ùù< =
)
ùù= >
{
ûû 
errors
üü 
.
üü 
Add
üü 
(
üü 
string
üü !
.
üü! "
Format
üü" (
(
üü( )
ErrorMessages
üü) 6
.
üü6 7*
SocialLinkDomainMessageLabel
üü7 S
,
üüS T
platformName
üüU a
)
üüa b
)
üüb c
;
üüc d
}
†† 
return
°° 
errors
°° 
;
°° 
}
¢¢ 	
}
££ 
}§§ é(
ÅC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Utilities\SoundManager.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Utils 
{ 
public 

static 
class 
SoundManager $
{ 
private 
static 
MediaPlayer "
_player# *
;* +
public 
static 
void 
	PlaySound $
($ %
string% +
fileName, 4
,4 5
double6 <
volume= C
=D E
$numF I
)I J
{ 	
string 
	soundPath 
= 
Path #
.# $
Combine$ +
(+ ,
	AppDomain, 5
.5 6
CurrentDomain6 C
.C D
BaseDirectoryD Q
,Q R
$strS [
,[ \
fileName] e
)e f
;f g
try 
{ 
if 
( 
! 
File 
. 
Exists  
(  !
	soundPath! *
)* +
)+ ,
throw 
new !
FileNotFoundException 3
(3 4
$str4 U
,U V
	soundPathW `
)` a
;a b
var 
player 
= 
new  
MediaPlayer! ,
(, -
)- .
;. /
player 
. 
Open 
( 
new 
Uri  #
(# $
	soundPath$ -
,- .
UriKind/ 6
.6 7
Absolute7 ?
)? @
)@ A
;A B
player   
.   
Volume   
=   
volume    &
;  & '
player"" 
."" 

MediaEnded"" !
+=""" $
(""% &
s""& '
,""' (
e"") *
)""* +
=>"", .
{## 
try$$ 
{%% 
player&& 
.&& 
Stop&& #
(&&# $
)&&$ %
;&&% &
player'' 
.'' 
Close'' $
(''$ %
)''% &
;''& '
}(( 
catch)) 
()) 
	Exception)) $
closeEx))% ,
))), -
{** 
Debug++ 
.++ 
	WriteLine++ '
(++' (
$"++( *
$str++* M
{++M N
closeEx++N U
.++U V
Message++V ]
}++] ^
"++^ _
)++_ `
;++` a
},, 
}-- 
;-- 
player// 
.// 
Play// 
(// 
)// 
;// 
}00 
catch11 
(11 !
FileNotFoundException11 (
fnfEx11) .
)11. /
{22 
Debug33 
.33 
	WriteLine33 
(33  
$"33  "
$str33" ;
{33; <
fnfEx33< A
.33A B
FileName33B J
}33J K
"33K L
)33L M
;33M N
}44 
catch55 
(55 
UriFormatException55 %
uriEx55& +
)55+ ,
{66 
Debug77 
.77 
	WriteLine77 
(77  
$"77  "
$str77" =
{77= >
uriEx77> C
.77C D
Message77D K
}77K L
"77L M
)77M N
;77N O
}88 
catch99 
(99 %
InvalidOperationException99 ,
opEx99- 1
)991 2
{:: 
Debug;; 
.;; 
	WriteLine;; 
(;;  
$";;  "
$str;;" R
{;;R S
opEx;;S W
.;;W X
Message;;X _
};;_ `
";;` a
);;a b
;;;b c
}<< 
catch== 
(== 
	Exception== 
ex== 
)==  
{>> 
Debug?? 
.?? 
	WriteLine?? 
(??  
$"??  "
$str??" K
{??K L
ex??L N
.??N O
Message??O V
}??V W
"??W X
)??X Y
;??Y Z
}@@ 
}AA 	
publicGG 
staticGG 
voidGG 
	PlayClickGG $
(GG$ %
)GG% &
{HH 	
	PlaySoundII 
(II 
$strII '
,II' (
$numII) ,
)II, -
;II- .
}JJ 	
publicOO 
staticOO 
voidOO 
	PlayErrorOO $
(OO$ %
)OO% &
{PP 	
	PlaySoundQQ 
(QQ 
$strQQ '
,QQ' (
$numQQ) ,
)QQ, -
;QQ- .
}RR 	
publicWW 
staticWW 
voidWW 
PlaySuccessWW &
(WW& '
)WW' (
{XX 	
	PlaySoundYY 
(YY 
$strYY '
,YY' (
$numYY) ,
)YY, -
;YY- .
}ZZ 	
}[[ 
}\\ Ü*
óC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\CustomUserControls\VisiblePasswordField.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
{ 
public 

partial 
class  
VisiblePasswordField -
:. /
UserControl0 ;
{ 
private 
readonly 
Geometry !
_eyeOpen" *
=+ ,
Geometry- 5
.5 6
Parse6 ;
(; <
$str	< ç
)
ç é
;
é è
private 
readonly 
Geometry !

_eyeClosed" ,
=- .
Geometry/ 7
.7 8
Parse8 =
(= >
$str> t
)t u
;u v
private 
bool 
_isPasswordVisible '
=( )
false* /
;/ 0
public  
VisiblePasswordField #
(# $
)$ %
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
public   
static   
readonly   
DependencyProperty   1
PasswordProperty  2 B
=  C D
DependencyProperty!! 
.!! 
Register!! '
(!!' (
$str!!( 2
,!!2 3
typeof!!4 :
(!!: ;
string!!; A
)!!A B
,!!B C
typeof!!D J
(!!J K 
VisiblePasswordField!!K _
)!!_ `
,!!` a
new"" %
FrameworkPropertyMetadata"" )
("") *
string""* 0
.""0 1
Empty""1 6
,""6 7,
 FrameworkPropertyMetadataOptions""8 X
.""X Y 
BindsTwoWayByDefault""Y m
)""m n
)""n o
;""o p
public$$ 
string$$ 
Password$$ 
{%% 	
get&& 
=>&& 
(&& 
string&& 
)&& 
GetValue&& #
(&&# $
PasswordProperty&&$ 4
)&&4 5
;&&5 6
set'' 
=>'' 
SetValue'' 
('' 
PasswordProperty'' -
,''- .
value''/ 4
)''4 5
;''5 6
}(( 	
private** 
void** &
VisiblePasswordTextChanged** /
(**/ 0
object**0 6
sender**7 =
,**= > 
TextChangedEventArgs**? S
e**T U
)**U V
{++ 	
Password,, 
=,, "
VisiblePasswordTextBox,, -
.,,- .
Text,,. 2
;,,2 3
}-- 	
private// 
void// 
PasswordChanged// $
(//$ %
object//% +
sender//, 2
,//2 3
RoutedEventArgs//4 C
e//D E
)//E F
{00 	
Password11 
=11 
PasswordBox11 "
.11" #
Password11# +
;11+ ,
}22 	
private44 
void44 %
ClickTogglePasswordButton44 .
(44. /
object44/ 5
sender446 <
,44< =
RoutedEventArgs44> M
e44N O
)44O P
{55 	
if66 
(66 
_isPasswordVisible66 "
)66" #
{77 
PasswordBox88 
.88 
Password88 $
=88% &"
VisiblePasswordTextBox88' =
.88= >
Text88> B
;88B C"
VisiblePasswordTextBox99 &
.99& '

Visibility99' 1
=992 3

Visibility994 >
.99> ?
	Collapsed99? H
;99H I
PasswordBox:: 
.:: 

Visibility:: &
=::' (

Visibility::) 3
.::3 4
Visible::4 ;
;::; <
EyeIcon;; 
.;; 
Data;; 
=;; 
_eyeOpen;; '
;;;' (
EyeIcon<< 
.<< 
Fill<< 
=<< 
new<< "
SolidColorBrush<<# 2
(<<2 3
Color<<3 8
.<<8 9
FromRgb<<9 @
(<<@ A
$num<<A D
,<<D E
$num<<F I
,<<I J
$num<<K N
)<<N O
)<<O P
;<<P Q
}== 
else>> 
{?? "
VisiblePasswordTextBox@@ &
.@@& '
Text@@' +
=@@, -
PasswordBox@@. 9
.@@9 :
Password@@: B
;@@B C
PasswordBoxAA 
.AA 

VisibilityAA &
=AA' (

VisibilityAA) 3
.AA3 4
	CollapsedAA4 =
;AA= >"
VisiblePasswordTextBoxBB &
.BB& '

VisibilityBB' 1
=BB2 3

VisibilityBB4 >
.BB> ?
VisibleBB? F
;BBF G
EyeIconCC 
.CC 
DataCC 
=CC 

_eyeClosedCC )
;CC) *
EyeIconDD 
.DD 
FillDD 
=DD 
newDD "
SolidColorBrushDD# 2
(DD2 3
ColorDD3 8
.DD8 9
FromRgbDD9 @
(DD@ A
$numDDA C
,DDC D
$numDDE H
,DDH I
$numDDJ M
)DDM N
)DDN O
;DDO P
}EE 
_isPasswordVisibleFF 
=FF  
!FF! "
_isPasswordVisibleFF" 4
;FF4 5
}GG 	
}HH 
}II ˛
èC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\PopUpWindows\GottenAvatarWindow.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
PopUpWindows &
{ 
public 

partial 
class 
AvatarGottenWindow +
:, -
Window. 4
{ 
public 
AvatarGottenWindow !
(! "
)" #
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
} 
} Ó
àC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\SelectAnAvatarPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
SelectAnAvatarPage +
:, -
Page. 2
{ 
public 
SelectAnAvatarPage !
(! "
)" #
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
} 
} ”£
àC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\UnoLisWindows\MainWindow.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
{ 
public 

partial 
class 

MainWindow #
:$ %
Window& ,
,, -"
ILogoutManagerCallback. D
{ 
private 
LogoutManagerClient #
_logoutClient$ 1
;1 2
public   

MainWindow   
(   
)   
{!! 	
InitializeComponent"" 
(""  
)""  !
;""! "
}## 	
public%% 
void%% 
LogoutResponse%% "
(%%" #
bool%%# '
success%%( /
,%%/ 0
string%%1 7
message%%8 ?
)%%? @
{&& 	
Application'' 
.'' 
Current'' 
.''  

Dispatcher''  *
.''* +
Invoke''+ 1
(''1 2
(''2 3
)''3 4
=>''5 7
{(( 
if)) 
()) 
success)) 
))) 
{** 
new++ 
SimplePopUpWindow++ )
(++) *
Global++* 0
.++0 1
SuccessLabel++1 =
,++= >
message++? F
)++F G
.++G H

ShowDialog++H R
(++R S
)++S T
;++T U
},, 
else-- 
{.. 
new// 
SimplePopUpWindow// )
(//) *
Global//* 0
.//0 1
UnsuccessfulLabel//1 B
,//B C
message//D K
)//K L
.//L M

ShowDialog//M W
(//W X
)//X Y
;//Y Z
}00 
}11 
)11 
;11 
}22 	
private44 
void44 &
VideoBackground_MediaEnded44 /
(44/ 0
object440 6
sender447 =
,44= >
RoutedEventArgs44? N
e44O P
)44P Q
{55 	
VideoBackground77 
.77 
Position77 $
=77% &
TimeSpan77' /
.77/ 0
Zero770 4
;774 5
VideoBackground88 
.88 
Play88  
(88  !
)88! "
;88" #
}99 	
public;; 
void;; 
SetMusicVolume;; "
(;;" #
double;;# )
volume;;* 0
);;0 1
{<< 	
MusicPlayer>> 
.>> 
Volume>> 
=>>  
volume>>! '
/>>( )
$num>>* /
;>>/ 0
}?? 	
privateAA 
asyncAA 
voidAA 
MainWindow_LoadedAA ,
(AA, -
objectAA- 3
senderAA4 :
,AA: ;
RoutedEventArgsAA< K
eAAL M
)AAM N
{BB 	
	MainFrameDD 
.DD 
NavigateDD 
(DD 
newDD "
PagesDD# (
.DD( )
GamePageDD) 1
(DD1 2
)DD2 3
)DD3 4
;DD4 5
	IntroMaskGG 
.GG 

VisibilityGG  
=GG! "

VisibilityGG# -
.GG- .
VisibleGG. 5
;GG5 6
	IntroMaskHH 
.HH 
OpacityHH 
=HH 
$numHH  !
;HH! "

SplashLogoII 
.II 

VisibilityII !
=II" #

VisibilityII$ .
.II. /
VisibleII/ 6
;II6 7

SplashLogoJJ 
.JJ 
OpacityJJ 
=JJ  
$numJJ! "
;JJ" #
varMM 

fadeInLogoMM 
=MM 
newMM  
DoubleAnimationMM! 0
{NN 
FromOO 
=OO 
$numOO 
,OO 
ToPP 
=PP 
$numPP 
,PP 
DurationQQ 
=QQ 
TimeSpanQQ #
.QQ# $
FromSecondsQQ$ /
(QQ/ 0
$numQQ0 3
)QQ3 4
,QQ4 5
EasingFunctionRR 
=RR  
newRR! $
	CubicEaseRR% .
{RR/ 0

EasingModeRR1 ;
=RR< =

EasingModeRR> H
.RRH I
	EaseInOutRRI R
}RRS T
}SS 
;SS 
varUU 
	scaleLogoUU 
=UU 
newUU 
DoubleAnimationUU  /
{VV 
FromWW 
=WW 
$numWW 
,WW 
ToXX 
=XX 
$numXX 
,XX 
DurationYY 
=YY 
TimeSpanYY #
.YY# $
FromSecondsYY$ /
(YY/ 0
$numYY0 3
)YY3 4
,YY4 5
EasingFunctionZZ 
=ZZ  
newZZ! $
	CubicEaseZZ% .
{ZZ/ 0

EasingModeZZ1 ;
=ZZ< =

EasingModeZZ> H
.ZZH I
EaseOutZZI P
}ZZQ R
}[[ 
;[[ 

SplashLogo]] 
.]] 
BeginAnimation]] %
(]]% &
	UIElement]]& /
.]]/ 0
OpacityProperty]]0 ?
,]]? @

fadeInLogo]]A K
)]]K L
;]]L M

SplashLogo^^ 
.^^ 
RenderTransform^^ &
.^^& '
BeginAnimation^^' 5
(^^5 6
ScaleTransform^^6 D
.^^D E
ScaleXProperty^^E S
,^^S T
	scaleLogo^^U ^
)^^^ _
;^^_ `

SplashLogo__ 
.__ 
RenderTransform__ &
.__& '
BeginAnimation__' 5
(__5 6
ScaleTransform__6 D
.__D E
ScaleYProperty__E S
,__S T
	scaleLogo__U ^
)__^ _
;___ `
awaitaa 
Taskaa 
.aa 
Delayaa 
(aa 
$numaa !
)aa! "
;aa" #
vardd 
fadeOutLogodd 
=dd 
newdd !
DoubleAnimationdd" 1
{ee 
Fromff 
=ff 
$numff 
,ff 
Togg 
=gg 
$numgg 
,gg 
Durationhh 
=hh 
TimeSpanhh #
.hh# $
FromSecondshh$ /
(hh/ 0
$numhh0 3
)hh3 4
,hh4 5
EasingFunctionii 
=ii  
newii! $
	CubicEaseii% .
{ii/ 0

EasingModeii1 ;
=ii< =

EasingModeii> H
.iiH I
	EaseInOutiiI R
}iiS T
}jj 
;jj 

SplashLogokk 
.kk 
BeginAnimationkk %
(kk% &
	UIElementkk& /
.kk/ 0
OpacityPropertykk0 ?
,kk? @
fadeOutLogokkA L
)kkL M
;kkM N
awaitmm 
Taskmm 
.mm 
Delaymm 
(mm 
$nummm !
)mm! "
;mm" #
MusicPlayernn 
.nn 
Playnn 
(nn 
)nn 
;nn 
varqq 
fadeOutMaskqq 
=qq 
newqq !
DoubleAnimationqq" 1
{rr 
Fromss 
=ss 
$numss 
,ss 
Tott 
=tt 
$numtt 
,tt 
Durationuu 
=uu 
TimeSpanuu #
.uu# $
FromSecondsuu$ /
(uu/ 0
$numuu0 3
)uu3 4
,uu4 5
EasingFunctionvv 
=vv  
newvv! $
	CubicEasevv% .
{vv/ 0

EasingModevv1 ;
=vv< =

EasingModevv> H
.vvH I
EaseOutvvI P
}vvQ R
}ww 
;ww 
fadeOutMaskyy 
.yy 
	Completedyy !
+=yy" $
(yy% &
syy& '
,yy' (
_yy) *
)yy* +
=>yy, .
{zz 
	IntroMask{{ 
.{{ 

Visibility{{ $
={{% &

Visibility{{' 1
.{{1 2
	Collapsed{{2 ;
;{{; <

SplashLogo|| 
.|| 

Visibility|| %
=||& '

Visibility||( 2
.||2 3
	Collapsed||3 <
;||< =
}}} 
;}} 
	IntroMask 
. 
BeginAnimation $
($ %
	UIElement% .
.. /
OpacityProperty/ >
,> ?
fadeOutMask@ K
)K L
;L M
}
ÄÄ 	
private
ÇÇ 
void
ÇÇ $
MusicPlayer_MediaEnded
ÇÇ +
(
ÇÇ+ ,
object
ÇÇ, 2
sender
ÇÇ3 9
,
ÇÇ9 :
RoutedEventArgs
ÇÇ; J
e
ÇÇK L
)
ÇÇL M
{
ÉÉ 	
MusicPlayer
ÖÖ 
.
ÖÖ 
Position
ÖÖ  
=
ÖÖ! "
TimeSpan
ÖÖ# +
.
ÖÖ+ ,
Zero
ÖÖ, 0
;
ÖÖ0 1
MusicPlayer
ÜÜ 
.
ÜÜ 
Play
ÜÜ 
(
ÜÜ 
)
ÜÜ 
;
ÜÜ 
}
áá 	
public
ää 
async
ää 
void
ää  
SetBackgroundMedia
ää ,
(
ää, -
string
ää- 3
	videoPath
ää4 =
,
ää= >
string
ää? E
	musicPath
ääF O
)
ääO P
{
ãã 	
try
åå 
{
çç 
var
èè 
fadeOut
èè 
=
èè 
new
èè !
DoubleAnimation
èè" 1
{
êê 
From
ëë 
=
ëë 
$num
ëë 
,
ëë 
To
íí 
=
íí 
$num
íí 
,
íí 
Duration
ìì 
=
ìì 
TimeSpan
ìì '
.
ìì' (
FromSeconds
ìì( 3
(
ìì3 4
$num
ìì4 7
)
ìì7 8
,
ìì8 9
EasingFunction
îî "
=
îî# $
new
îî% (
	CubicEase
îî) 2
{
îî3 4

EasingMode
îî5 ?
=
îî@ A

EasingMode
îîB L
.
îîL M
	EaseInOut
îîM V
}
îîW X
}
ïï 
;
ïï 
VideoBackground
óó 
.
óó  
BeginAnimation
óó  .
(
óó. /
	UIElement
óó/ 8
.
óó8 9
OpacityProperty
óó9 H
,
óóH I
fadeOut
óóJ Q
)
óóQ R
;
óóR S
double
òò 
	oldVolume
òò  
=
òò! "
MusicPlayer
òò# .
.
òò. /
Volume
òò/ 5
;
òò5 6
var
öö 
fadeOutMusic
öö  
=
öö! "
new
öö# &
DoubleAnimation
öö' 6
{
õõ 
From
úú 
=
úú 
	oldVolume
úú $
,
úú$ %
To
ùù 
=
ùù 
$num
ùù 
,
ùù 
Duration
ûû 
=
ûû 
TimeSpan
ûû '
.
ûû' (
FromSeconds
ûû( 3
(
ûû3 4
$num
ûû4 7
)
ûû7 8
,
ûû8 9
EasingFunction
üü "
=
üü# $
new
üü% (
	CubicEase
üü) 2
{
üü3 4

EasingMode
üü5 ?
=
üü@ A

EasingMode
üüB L
.
üüL M
	EaseInOut
üüM V
}
üüW X
}
†† 
;
†† 
MusicPlayer
°° 
.
°° 
BeginAnimation
°° *
(
°°* +
MediaElement
°°+ 7
.
°°7 8
VolumeProperty
°°8 F
,
°°F G
fadeOutMusic
°°H T
)
°°T U
;
°°U V
await
££ 
Task
££ 
.
££ 
Delay
££  
(
££  !
$num
££! %
)
££% &
;
££& '
VideoBackground
¶¶ 
.
¶¶  
Stop
¶¶  $
(
¶¶$ %
)
¶¶% &
;
¶¶& '
MusicPlayer
ßß 
.
ßß 
Stop
ßß  
(
ßß  !
)
ßß! "
;
ßß" #
VideoBackground
©© 
.
©©  
Source
©©  &
=
©©' (
new
©©) ,
Uri
©©- 0
(
©©0 1
System
©©1 7
.
©©7 8
IO
©©8 :
.
©©: ;
Path
©©; ?
.
©©? @
GetFullPath
©©@ K
(
©©K L
	videoPath
©©L U
)
©©U V
)
©©V W
;
©©W X
MusicPlayer
™™ 
.
™™ 
Source
™™ "
=
™™# $
new
™™% (
Uri
™™) ,
(
™™, -
System
™™- 3
.
™™3 4
IO
™™4 6
.
™™6 7
Path
™™7 ;
.
™™; <
GetFullPath
™™< G
(
™™G H
	musicPath
™™H Q
)
™™Q R
)
™™R S
;
™™S T
VideoBackground
≠≠ 
.
≠≠  
Play
≠≠  $
(
≠≠$ %
)
≠≠% &
;
≠≠& '
MusicPlayer
ÆÆ 
.
ÆÆ 
Play
ÆÆ  
(
ÆÆ  !
)
ÆÆ! "
;
ÆÆ" #
var
±± 
fadeIn
±± 
=
±± 
new
±±  
DoubleAnimation
±±! 0
{
≤≤ 
From
≥≥ 
=
≥≥ 
$num
≥≥ 
,
≥≥ 
To
¥¥ 
=
¥¥ 
$num
¥¥ 
,
¥¥ 
Duration
µµ 
=
µµ 
TimeSpan
µµ '
.
µµ' (
FromSeconds
µµ( 3
(
µµ3 4
$num
µµ4 7
)
µµ7 8
,
µµ8 9
EasingFunction
∂∂ "
=
∂∂# $
new
∂∂% (
	CubicEase
∂∂) 2
{
∂∂3 4

EasingMode
∂∂5 ?
=
∂∂@ A

EasingMode
∂∂B L
.
∂∂L M
	EaseInOut
∂∂M V
}
∂∂W X
}
∑∑ 
;
∑∑ 
VideoBackground
∏∏ 
.
∏∏  
BeginAnimation
∏∏  .
(
∏∏. /
	UIElement
∏∏/ 8
.
∏∏8 9
OpacityProperty
∏∏9 H
,
∏∏H I
fadeIn
∏∏J P
)
∏∏P Q
;
∏∏Q R
var
∫∫ 
fadeInMusic
∫∫ 
=
∫∫  !
new
∫∫" %
DoubleAnimation
∫∫& 5
{
ªª 
From
ºº 
=
ºº 
$num
ºº 
,
ºº 
To
ΩΩ 
=
ΩΩ 
	oldVolume
ΩΩ "
,
ΩΩ" #
Duration
ææ 
=
ææ 
TimeSpan
ææ '
.
ææ' (
FromSeconds
ææ( 3
(
ææ3 4
$num
ææ4 7
)
ææ7 8
,
ææ8 9
EasingFunction
øø "
=
øø# $
new
øø% (
	CubicEase
øø) 2
{
øø3 4

EasingMode
øø5 ?
=
øø@ A

EasingMode
øøB L
.
øøL M
	EaseInOut
øøM V
}
øøW X
}
¿¿ 
;
¿¿ 
MusicPlayer
¡¡ 
.
¡¡ 
BeginAnimation
¡¡ *
(
¡¡* +
MediaElement
¡¡+ 7
.
¡¡7 8
VolumeProperty
¡¡8 F
,
¡¡F G
fadeInMusic
¡¡H S
)
¡¡S T
;
¡¡T U
}
¬¬ 
catch
√√ 
(
√√ 
	Exception
√√ 
ex
√√ 
)
√√  
{
ƒƒ 

MessageBox
≈≈ 
.
≈≈ 
Show
≈≈ 
(
≈≈  
$"
≈≈  "
$str
≈≈" C
{
≈≈C D
ex
≈≈D F
.
≈≈F G
Message
≈≈G N
}
≈≈N O
"
≈≈O P
,
≈≈P Q
$str
≈≈R [
,
≈≈[ \
MessageBoxButton
≈≈] m
.
≈≈m n
OK
≈≈n p
,
≈≈p q
MessageBoxImage≈≈r Å
.≈≈Å Ç
Error≈≈Ç á
)≈≈á à
;≈≈à â
}
∆∆ 
}
«« 	
public
…… 
async
…… 
void
…… &
RestoreDefaultBackground
…… 2
(
……2 3
)
……3 4
{
   	
try
ÀÀ 
{
ÃÃ 
string
ÕÕ 
defaultVideo
ÕÕ #
=
ÕÕ$ %
System
ÕÕ& ,
.
ÕÕ, -
IO
ÕÕ- /
.
ÕÕ/ 0
Path
ÕÕ0 4
.
ÕÕ4 5
Combine
ÕÕ5 <
(
ÕÕ< =
	AppDomain
ÕÕ= F
.
ÕÕF G
CurrentDomain
ÕÕG T
.
ÕÕT U
BaseDirectory
ÕÕU b
,
ÕÕb c
$str
ÕÕd }
)
ÕÕ} ~
;
ÕÕ~ 
string
ŒŒ 
defaultMusic
ŒŒ #
=
ŒŒ$ %
System
ŒŒ& ,
.
ŒŒ, -
IO
ŒŒ- /
.
ŒŒ/ 0
Path
ŒŒ0 4
.
ŒŒ4 5
Combine
ŒŒ5 <
(
ŒŒ< =
	AppDomain
ŒŒ= F
.
ŒŒF G
CurrentDomain
ŒŒG T
.
ŒŒT U
BaseDirectory
ŒŒU b
,
ŒŒb c
$str
ŒŒd 
)ŒŒ Ä
;ŒŒÄ Å
await
–– 
Task
–– 
.
–– 
Delay
––  
(
––  !
$num
––! $
)
––$ %
;
––% &
VideoBackground
““ 
.
““  
Stop
““  $
(
““$ %
)
““% &
;
““& '
MusicPlayer
”” 
.
”” 
Stop
””  
(
””  !
)
””! "
;
””" #
VideoBackground
’’ 
.
’’  
Source
’’  &
=
’’' (
new
’’) ,
Uri
’’- 0
(
’’0 1
defaultVideo
’’1 =
,
’’= >
UriKind
’’? F
.
’’F G
Absolute
’’G O
)
’’O P
;
’’P Q
MusicPlayer
÷÷ 
.
÷÷ 
Source
÷÷ "
=
÷÷# $
new
÷÷% (
Uri
÷÷) ,
(
÷÷, -
defaultMusic
÷÷- 9
,
÷÷9 :
UriKind
÷÷; B
.
÷÷B C
Absolute
÷÷C K
)
÷÷K L
;
÷÷L M
VideoBackground
ÿÿ 
.
ÿÿ  
Play
ÿÿ  $
(
ÿÿ$ %
)
ÿÿ% &
;
ÿÿ& '
MusicPlayer
ŸŸ 
.
ŸŸ 
Play
ŸŸ  
(
ŸŸ  !
)
ŸŸ! "
;
ŸŸ" #
}
⁄⁄ 
catch
€€ 
(
€€ 
	Exception
€€ 
ex
€€ 
)
€€  
{
‹‹ 

MessageBox
›› 
.
›› 
Show
›› 
(
››  
$"
››  "
$str
››" ?
{
››? @
ex
››@ B
.
››B C
Message
››C J
}
››J K
"
››K L
,
››L M
$str
››N W
,
››W X
MessageBoxButton
››Y i
.
››i j
OK
››j l
,
››l m
MessageBoxImage
››n }
.
››} ~
Error››~ É
)››É Ñ
;››Ñ Ö
}
ﬁﬁ 
}
ﬂﬂ 	
private
·· 
void
·· 
LogoutCurrentUser
·· &
(
··& '
)
··' (
{
‚‚ 	
try
„„ 
{
‰‰ 
if
ÂÂ 
(
ÂÂ 
!
ÂÂ 
string
ÂÂ 
.
ÂÂ  
IsNullOrWhiteSpace
ÂÂ .
(
ÂÂ. /
CurrentSession
ÂÂ/ =
.
ÂÂ= >!
CurrentUserNickname
ÂÂ> Q
)
ÂÂQ R
)
ÂÂR S
{
ÊÊ 
var
ÁÁ 
context
ÁÁ 
=
ÁÁ  !
new
ÁÁ" %
InstanceContext
ÁÁ& 5
(
ÁÁ5 6
this
ÁÁ6 :
)
ÁÁ: ;
;
ÁÁ; <
_logoutClient
ËË !
=
ËË" #
new
ËË$ '!
LogoutManagerClient
ËË( ;
(
ËË; <
context
ËË< C
)
ËËC D
;
ËËD E
_logoutClient
ÈÈ !
.
ÈÈ! "
LogoutAsync
ÈÈ" -
(
ÈÈ- .
CurrentSession
ÈÈ. <
.
ÈÈ< =!
CurrentUserNickname
ÈÈ= P
)
ÈÈP Q
;
ÈÈQ R
CurrentSession
ÎÎ "
.
ÎÎ" #!
CurrentUserNickname
ÎÎ# 6
=
ÎÎ7 8
null
ÎÎ9 =
;
ÎÎ= >
CurrentSession
ÏÏ "
.
ÏÏ" #$
CurrentUserProfileData
ÏÏ# 9
=
ÏÏ: ;
null
ÏÏ< @
;
ÏÏ@ A
}
ÌÌ 
}
ÓÓ 
catch
ÔÔ 
(
ÔÔ 
	Exception
ÔÔ 
ex
ÔÔ 
)
ÔÔ  
{
 
new
ÒÒ 
SimplePopUpWindow
ÒÒ %
(
ÒÒ% &
Global
ÒÒ& ,
.
ÒÒ, -
UnsuccessfulLabel
ÒÒ- >
,
ÒÒ> ?
ex
ÒÒ@ B
.
ÒÒB C
Message
ÒÒC J
)
ÒÒJ K
.
ÒÒK L

ShowDialog
ÒÒL V
(
ÒÒV W
)
ÒÒW X
;
ÒÒX Y
}
ÚÚ 
}
ÛÛ 	
private
ıı 
void
ıı 
MainWindowClosing
ıı &
(
ıı& '
object
ıı' -
sender
ıı. 4
,
ıı4 5
System
ıı6 <
.
ıı< =
ComponentModel
ıı= K
.
ııK L
CancelEventArgs
ııL [
e
ıı\ ]
)
ıı] ^
{
ˆˆ 	
var
˜˜ 
result
˜˜ 
=
˜˜ 
new
˜˜ !
QuestionPopUpWindow
˜˜ 0
(
˜˜0 1
Global
˜˜1 7
.
˜˜7 8
ConfirmationLabel
˜˜8 I
,
˜˜I J
Global
˜˜K Q
.
˜˜Q R 
LogoutMessageLabel
˜˜R d
)
˜˜d e
.
˜˜e f

ShowDialog
˜˜f p
(
˜˜p q
)
˜˜q r
;
˜˜r s
if
¯¯ 
(
¯¯ 
result
¯¯ 
==
¯¯ 
true
¯¯ 
)
¯¯ 
{
˘˘ 
LogoutCurrentUser
˙˙ !
(
˙˙! "
)
˙˙" #
;
˙˙# $
}
˚˚ 
else
¸¸ 
{
˝˝ 
e
˛˛ 
.
˛˛ 
Cancel
˛˛ 
=
˛˛ 
true
˛˛ 
;
˛˛  
}
ˇˇ 
}
ÄÄ 	
}
ÅÅ 
}ÇÇ Î
ÖC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\YourProfilePage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
YourProfilePage (
:) *
Page+ /
,/ 0'
IProfileViewManagerCallback1 L
{ 
private $
ProfileViewManagerClient (
_profileViewClient) ;
;; <
private   
LoadingPopUpWindow   "
_loadingPopUpWindow  # 6
;  6 7
public"" 
YourProfilePage"" 
("" 
)""  
{## 	
InitializeComponent$$ 
($$  
)$$  !
;$$! "
RequestProfileData%% 
(%% 
)%%  
;%%  !
}&& 	
public(( 
void(( 
ProfileDataReceived(( '
(((' (
bool((( ,
success((- 4
,((4 5
ProfileData((6 A
data((B F
)((F G
{)) 	
Application** 
.** 
Current** 
.**  

Dispatcher**  *
.*** +
Invoke**+ 1
(**1 2
(**2 3
)**3 4
=>**5 7
{++ 
_loadingPopUpWindow,, #
?,,# $
.,,$ %
StopLoadingAndClose,,% 8
(,,8 9
),,9 :
;,,: ;
if-- 
(-- 
!-- 
success-- 
||-- 
data--  $
==--% '
null--( ,
)--, -
{.. 
new// 
SimplePopUpWindow// )
(//) *
Global//* 0
.//0 1
WarningLabel//1 =
,//= >
$str//? A
)//A B
.//B C

ShowDialog//C M
(//M N
)//N O
;//O P
LoadDefaultData00 #
(00# $
)00$ %
;00% &
return11 
;11 
}22 
var44 
clientProfile44 !
=44" #
data44$ (
.44( )
ToClientModel44) 6
(446 7
)447 8
;448 9
UserNicknameLabel66 !
.66! "
Text66" &
=66' (
data66) -
.66- .
Nickname66. 6
;666 7
UserFullNameLabel77 !
.77! "
Text77" &
=77' (
data77) -
.77- .
FullName77. 6
;776 7
UserEmailLabel88 
.88 
Text88 #
=88$ %
data88& *
.88* +
Email88+ 0
;880 1
UserFacebookLink::  
.::  !
NavigateUri::! ,
=::- .
!::/ 0
string::0 6
.::6 7
IsNullOrWhiteSpace::7 I
(::I J
data::J N
.::N O
FacebookUrl::O Z
)::Z [
?::\ ]
new::^ a
Uri::b e
(::e f
data::f j
.::j k
FacebookUrl::k v
)::v w
:::x y
null::z ~
;::~ 
UserInstagramLink;; !
.;;! "
NavigateUri;;" -
=;;. /
!;;0 1
string;;1 7
.;;7 8
IsNullOrWhiteSpace;;8 J
(;;J K
data;;K O
.;;O P
InstagramUrl;;P \
);;\ ]
?;;^ _
new;;` c
Uri;;d g
(;;g h
data;;h l
.;;l m
InstagramUrl;;m y
);;y z
:;;{ |
null	;;} Å
;
;;Å Ç
UserTikTokLink<< 
.<< 
NavigateUri<< *
=<<+ ,
!<<- .
string<<. 4
.<<4 5
IsNullOrWhiteSpace<<5 G
(<<G H
data<<H L
.<<L M
	TikTokUrl<<M V
)<<V W
?<<X Y
new<<Z ]
Uri<<^ a
(<<a b
data<<b f
.<<f g
	TikTokUrl<<g p
)<<p q
:<<r s
null<<t x
;<<x y$
PlayerStatisticsDataGrid>> (
.>>( )
ItemsSource>>) 4
=>>5 6
new>>7 :
List>>; ?
<>>? @
dynamic>>@ G
>>>G H
{?? 
new@@ 
{AA 
MatchesPlayedBB %
=BB& '
dataBB( ,
.BB, -
MatchesPlayedBB- :
,BB: ;
WinsCC 
=CC 
dataCC #
.CC# $
WinsCC$ (
,CC( )
LosesDD 
=DD 
dataDD  $
.DD$ %
LossesDD% +
,DD+ ,
GlobalPointsEE $
=EE% &
dataEE' +
.EE+ ,
ExperiencePointsEE, <
,EE< =
WinRateFF 
=FF  !
dataFF" &
.FF& '
MatchesPlayedFF' 4
==FF5 7
$numFF8 9
?FF: ;
$strFF< @
:FFA B
$"GG 
{GG 
(GG 
intGG 
)GG  
(GG  !
floatGG! &
)GG& '
dataGG( ,
.GG, -
WinsGG- 1
/GG2 3
dataGG4 8
.GG8 9
MatchesPlayedGG9 F
*GGG H
$numGGI L
}GGL M
$strGGM N
"GGN O
}HH 
}II 
;II 
ChangeAvatarButtonKK "
.KK" #
	IsEnabledKK# ,
=KK- .
trueKK/ 3
;KK3 4
ChangeDataButtonLL  
.LL  !
	IsEnabledLL! *
=LL+ ,
trueLL- 1
;LL1 2
CurrentSessionMM 
.MM "
CurrentUserProfileDataMM 5
=MM6 7
clientProfileMM8 E
;MME F
}NN 
)NN 
;NN 
}OO 	
privateQQ 
voidQQ #
ClickChangeAvatarButtonQQ ,
(QQ, -
objectQQ- 3
senderQQ4 :
,QQ: ;
RoutedEventArgsQQ< K
eQQL M
)QQM N
{RR 	
SoundManagerSS 
.SS 
	PlayClickSS "
(SS" #
)SS# $
;SS$ %
NavigationServiceTT 
?TT 
.TT 
NavigateTT '
(TT' (
newTT( +
AvatarSelectionPageTT, ?
(TT? @
)TT@ A
)TTA B
;TTB C
}UU 	
privateWW 
voidWW !
ClickChangeDataButtonWW *
(WW* +
objectWW+ 1
senderWW2 8
,WW8 9
RoutedEventArgsWW: I
eWWJ K
)WWK L
{XX 	
SoundManagerYY 
.YY 
	PlayClickYY "
(YY" #
)YY# $
;YY$ %
ifZZ 
(ZZ 
CurrentSessionZZ 
.ZZ "
CurrentUserProfileDataZZ 5
==ZZ6 8
nullZZ9 =
)ZZ= >
{[[ 
new\\ 
SimplePopUpWindow\\ %
(\\% &
Global\\& ,
.\\, -
WarningLabel\\- 9
,\\9 :
ErrorMessages\\; H
.\\H I(
ProfileNotLoadedMessageLabel\\I e
)\\e f
.\\f g

ShowDialog\\g q
(\\q r
)\\r s
;\\s t
return]] 
;]] 
}^^ 
NavigationService__ 
?__ 
.__ 
Navigate__ '
(__' (
new__( +
EditProfilePage__, ;
(__; <
CurrentSession__< J
.__J K"
CurrentUserProfileData__K a
)__a b
)__b c
;__c d
}`` 	
privatebb 
voidbb 
ClickBackButtonbb $
(bb$ %
objectbb% +
senderbb, 2
,bb2 3
RoutedEventArgsbb4 C
ebbD E
)bbE F
{cc 	
SoundManagerdd 
.dd 
	PlayClickdd "
(dd" #
)dd# $
;dd$ %
NavigationServiceee 
?ee 
.ee 
Navigateee '
(ee' (
newee( +
MainMenuPageee, 8
(ee8 9
)ee9 :
)ee: ;
;ee; <
}ff 	
privatehh 
voidhh 
RequestProfileDatahh '
(hh' (
)hh( )
{ii 	
ClearUiBeforeLoadjj 
(jj 
)jj 
;jj  
ifkk 
(kk 
stringkk 
.kk 
IsNullOrWhiteSpacekk )
(kk) *
CurrentSessionkk* 8
.kk8 9
CurrentUserNicknamekk9 L
)kkL M
||kkN P
IsGuestkkQ X
(kkX Y
)kkY Z
)kkZ [
{ll 
LoadDefaultDatamm 
(mm  
)mm  !
;mm! "
returnnn 
;nn 
}oo 
tryqq 
{rr 
_loadingPopUpWindowss #
=ss$ %
newss& )
LoadingPopUpWindowss* <
(ss< =
)ss= >
{tt 
Owneruu 
=uu 
Windowuu "
.uu" #
	GetWindowuu# ,
(uu, -
thisuu- 1
)uu1 2
}vv 
;vv 
_loadingPopUpWindowww #
.ww# $
Showww$ (
(ww( )
)ww) *
;ww* +
varxx 
contextxx 
=xx 
newxx !
InstanceContextxx" 1
(xx1 2
thisxx2 6
)xx6 7
;xx7 8
_profileViewClientyy "
=yy# $
newyy% ($
ProfileViewManagerClientyy) A
(yyA B
contextyyB I
)yyI J
;yyJ K
_profileViewClientzz "
.zz" #
GetProfileDatazz# 1
(zz1 2
CurrentSessionzz2 @
.zz@ A
CurrentUserNicknamezzA T
)zzT U
;zzU V
}{{ 
catch|| 
(|| 
	Exception|| 
)|| 
{}} 
_loadingPopUpWindow~~ #
?~~# $
.~~$ %
StopLoadingAndClose~~% 8
(~~8 9
)~~9 :
;~~: ;
new 
SimplePopUpWindow %
(% &
Global& ,
., -
UnsuccessfulLabel- >
,> ?
ErrorMessages@ M
.M N'
ConnectionErrorMessageLabelN i
)i j
.j k

ShowDialogk u
(u v
)v w
;w x
LoadDefaultData
ÄÄ 
(
ÄÄ  
)
ÄÄ  !
;
ÄÄ! "
}
ÅÅ 
}
ÇÇ 	
private
ÑÑ 
bool
ÑÑ 
IsGuest
ÑÑ 
(
ÑÑ 
)
ÑÑ 
{
ÖÖ 	
return
ÜÜ 
string
ÜÜ 
.
ÜÜ 
Equals
ÜÜ  
(
ÜÜ  !
CurrentSession
ÜÜ! /
.
ÜÜ/ 0!
CurrentUserNickname
ÜÜ0 C
,
ÜÜC D
$str
ÜÜE L
,
ÜÜL M
StringComparison
ÜÜN ^
.
ÜÜ^ _
OrdinalIgnoreCase
ÜÜ_ p
)
ÜÜp q
;
ÜÜq r
}
áá 	
private
ââ 
void
ââ 
ClearUiBeforeLoad
ââ &
(
ââ& '
)
ââ' (
{
ää 	
UserNicknameLabel
ãã 
.
ãã 
Text
ãã "
=
ãã# $
$str
ãã% *
;
ãã* +
UserFullNameLabel
åå 
.
åå 
Text
åå "
=
åå# $
$str
åå% *
;
åå* +
UserEmailLabel
çç 
.
çç 
Text
çç 
=
çç  !
$str
çç" '
;
çç' (&
PlayerStatisticsDataGrid
éé $
.
éé$ %
ItemsSource
éé% 0
=
éé1 2
null
éé3 7
;
éé7 8
}
èè 	
private
ëë 
void
ëë 
LoadDefaultData
ëë $
(
ëë$ %
)
ëë% &
{
íí 	
var
ìì 
defaultData
ìì 
=
ìì 
new
ìì !
ProfileData
ìì" -
{
îî 
Nickname
ïï 
=
ïï 
$str
ïï "
,
ïï" #
FullName
ññ 
=
ññ 
$str
ññ 
,
ññ 
Email
óó 
=
óó 
$str
óó 
,
óó 
MatchesPlayed
òò 
=
òò 
$num
òò  !
,
òò! "
Wins
ôô 
=
ôô 
$num
ôô 
,
ôô 
Losses
öö 
=
öö 
$num
öö 
,
öö 
ExperiencePoints
õõ  
=
õõ! "
$num
õõ# $
}
úú 
;
úú 
UserNicknameLabel
ûû 
.
ûû 
Text
ûû "
=
ûû# $
defaultData
ûû% 0
.
ûû0 1
Nickname
ûû1 9
;
ûû9 :
UserFullNameLabel
üü 
.
üü 
Text
üü "
=
üü# $
defaultData
üü% 0
.
üü0 1
FullName
üü1 9
;
üü9 :
UserEmailLabel
†† 
.
†† 
Text
†† 
=
††  !
defaultData
††" -
.
††- .
Email
††. 3
;
††3 4&
PlayerStatisticsDataGrid
¢¢ $
.
¢¢$ %
ItemsSource
¢¢% 0
=
¢¢1 2
new
¢¢3 6
List
¢¢7 ;
<
¢¢; <
dynamic
¢¢< C
>
¢¢C D
{
££ 
new
§§ 
{
§§ 
MatchesPlayed
§§ #
=
§§$ %
$num
§§& '
,
§§' (
Wins
§§) -
=
§§. /
$num
§§0 1
,
§§1 2
Loses
§§3 8
=
§§9 :
$num
§§; <
,
§§< =
GlobalPoints
§§> J
=
§§K L
$num
§§M N
,
§§N O
WinRate
§§P W
=
§§X Y
$str
§§Z ^
}
§§_ `
}
•• 
;
••  
ChangeAvatarButton
ßß 
.
ßß 
	IsEnabled
ßß (
=
ßß) *
false
ßß+ 0
;
ßß0 1
ChangeDataButton
®® 
.
®® 
	IsEnabled
®® &
=
®®' (
false
®®) .
;
®®. /
}
©© 	
private
´´ 
void
´´ 
ClickSocialLink
´´ $
(
´´$ %
object
´´% +
sender
´´, 2
,
´´2 3
RoutedEventArgs
´´4 C
e
´´D E
)
´´E F
{
¨¨ 	
if
≠≠ 
(
≠≠ 
sender
≠≠ 
is
≠≠ 
	Hyperlink
≠≠ #
link
≠≠$ (
)
≠≠( )
{
ÆÆ 
string
ØØ 
target
ØØ 
=
ØØ 
GetSocialLinkUrl
ØØ  0
(
ØØ0 1
link
ØØ1 5
.
ØØ5 6
Name
ØØ6 :
)
ØØ: ;
;
ØØ; <
if
±± 
(
±± 
!
±± 
string
±± 
.
±±  
IsNullOrWhiteSpace
±± .
(
±±. /
target
±±/ 5
)
±±5 6
)
±±6 7
{
≤≤ 
BrowserHelper
≥≥ !
.
≥≥! "
OpenUrl
≥≥" )
(
≥≥) *
target
≥≥* 0
)
≥≥0 1
;
≥≥1 2
}
¥¥ 
else
µµ 
{
∂∂ 
new
∑∑ 
SimplePopUpWindow
∑∑ )
(
∑∑) *
Global
∑∑* 0
.
∑∑0 1
WarningLabel
∑∑1 =
,
∑∑= >
ErrorMessages
∏∏ %
.
∏∏% &4
&SocialNetworkNotConfiguredMessageLabel
∏∏& L
)
∏∏L M
.
ππ 

ShowDialog
ππ #
(
ππ# $
)
ππ$ %
;
ππ% &
}
∫∫ 
}
ªª 
}
ºº 	
private
ææ 
string
ææ 
GetSocialLinkUrl
ææ '
(
ææ' (
string
ææ( .
linkName
ææ/ 7
)
ææ7 8
{
øø 	
if
¿¿ 
(
¿¿ 
CurrentSession
¿¿ 
.
¿¿ $
CurrentUserProfileData
¿¿ 5
==
¿¿6 8
null
¿¿9 =
)
¿¿= >
{
¡¡ 
return
¬¬ 
null
¬¬ 
;
¬¬ 
}
√√ 
switch
≈≈ 
(
≈≈ 
linkName
≈≈ 
)
≈≈ 
{
∆∆ 
case
«« 
$str
«« '
:
««' (
return
»» 
CurrentSession
»» )
.
»») *$
CurrentUserProfileData
»»* @
.
»»@ A
FacebookUrl
»»A L
;
»»L M
case
…… 
$str
…… (
:
……( )
return
   
CurrentSession
   )
.
  ) *$
CurrentUserProfileData
  * @
.
  @ A
InstagramUrl
  A M
;
  M N
case
ÀÀ 
$str
ÀÀ %
:
ÀÀ% &
return
ÃÃ 
CurrentSession
ÃÃ )
.
ÃÃ) *$
CurrentUserProfileData
ÃÃ* @
.
ÃÃ@ A
	TikTokUrl
ÃÃA J
;
ÃÃJ K
default
ÕÕ 
:
ÕÕ 
return
ŒŒ 
null
ŒŒ 
;
ŒŒ  
}
œœ 
}
–– 	
}
—— 
}““ Ë
èC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\PopUpWindows\LoadingPopUpWindow.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
PopUpWindows &
{ 
public 

partial 
class 
LoadingPopUpWindow +
:, -
Window. 4
{ 
public 
LoadingPopUpWindow !
(! "
)" #
{ 	
InitializeComponent 
(  
)  !
;! "
Title 
= 
Global 
. 
LoadingLabel '
.' (
ToUpper( /
(/ 0
)0 1
;1 2
Loaded 
+= $
LoadingPopUpWindowLoaded .
;. /
} 	
public 
void 
StopLoadingAndClose '
(' (
)( )
{ 	

Dispatcher   
.   
Invoke   
(   
(   
)    
=>  ! #
{!! 
LoadingProgressBar"" "
.""" #
Stop""# '
(""' (
)""( )
;"") *
this## 
.## 
Close## 
(## 
)## 
;## 
}$$ 
)$$ 
;$$ 
}%% 	
private'' 
void'' $
LoadingPopUpWindowLoaded'' -
(''- .
object''. 4
sender''5 ;
,''; <
RoutedEventArgs''= L
e''M N
)''N O
{(( 	
LoadingProgressBar)) 
.)) 
Start)) $
())$ %
)))% &
;))& '
}** 	
}++ 
},, ã<
ÇC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\MainMenuPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
MainMenuPage %
:& '
Page( ,
,, -"
ILogoutManagerCallback. D
{ 
private 
LogoutManagerClient #
_logoutClient$ 1
;1 2
public 
MainMenuPage 
( 
) 
{   	
InitializeComponent!! 
(!!  
)!!  !
;!!! "
}"" 	
public$$ 
void$$ 
LogoutResponse$$ "
($$" #
bool$$# '
success$$( /
,$$/ 0
string$$1 7
message$$8 ?
)$$? @
{%% 	
Application&& 
.&& 
Current&& 
.&&  

Dispatcher&&  *
.&&* +
Invoke&&+ 1
(&&1 2
(&&2 3
)&&3 4
=>&&5 7
{'' 
if(( 
((( 
success(( 
)(( 
{)) 
new** 
SimplePopUpWindow** )
(**) *
Global*** 0
.**0 1
SuccessLabel**1 =
,**= >
message**? F
)**F G
.**G H

ShowDialog**H R
(**R S
)**S T
;**T U
}++ 
else,, 
{-- 
new.. 
SimplePopUpWindow.. )
(..) *
Global..* 0
...0 1
UnsuccessfulLabel..1 B
,..B C
message..D K
)..K L
...L M

ShowDialog..M W
(..W X
)..X Y
;..Y Z
}// 
}00 
)00 
;00 
}11 	
private33 
void33 
PlayButton_Click33 %
(33% &
object33& ,
sender33- 3
,333 4
RoutedEventArgs335 D
e33E F
)33F G
{44 	
SoundManager55 
.55 
	PlayClick55 "
(55" #
)55# $
;55$ %
NavigationService66 
?66 
.66 
Navigate66 '
(66' (
new66( +
MatchMenuPage66, 9
(669 :
)66: ;
)66; <
;66< =
}77 	
private99 
void99 *
SettingsLabel_MouseDoubleClick99 3
(993 4
object994 :
sender99; A
,99A B 
MouseButtonEventArgs99C W
e99X Y
)99Y Z
{:: 	
SoundManager;; 
.;; 
	PlayClick;; "
(;;" #
);;# $
;;;$ %
NavigationService<< 
?<< 
.<< 
Navigate<< '
(<<' (
new<<( +
SettingsPage<<, 8
(<<8 9
)<<9 :
)<<: ;
;<<; <
}== 	
private?? 
void?? &
ShopLabel_MouseDoubleClick?? /
(??/ 0
object??0 6
sender??7 =
,??= > 
MouseButtonEventArgs??? S
e??T U
)??U V
{@@ 	
SoundManagerAA 
.AA 
	PlayClickAA "
(AA" #
)AA# $
;AA$ %
NavigationServiceBB 
?BB 
.BB 
NavigateBB '
(BB' (
newBB( +
AvatarShopPageBB, :
(BB: ;
)BB; <
)BB< =
;BB= >
}CC 	
privateEE 
voidEE )
ProfileLabel_MouseDoubleClickEE 2
(EE2 3
objectEE3 9
senderEE: @
,EE@ A 
MouseButtonEventArgsEEB V
eEEW X
)EEX Y
{FF 	
SoundManagerGG 
.GG 
	PlayClickGG "
(GG" #
)GG# $
;GG$ %
NavigationServiceHH 
?HH 
.HH 
NavigateHH '
(HH' (
newHH( +
YourProfilePageHH, ;
(HH; <
)HH< =
)HH= >
;HH> ?
}II 	
privateKK 
voidKK &
ExitLabel_MouseDoubleClickKK /
(KK/ 0
objectKK0 6
senderKK7 =
,KK= > 
MouseButtonEventArgsKK? S
eKKT U
)KKU V
{LL 	
SoundManagerMM 
.MM 
	PlayClickMM "
(MM" #
)MM# $
;MM$ %
varNN 
resultNN 
=NN 
newNN 
QuestionPopUpWindowNN 0
(NN0 1
GlobalNN1 7
.NN7 8
ConfirmationLabelNN8 I
,NNI J
GlobalNNK Q
.NNQ R
LogoutMessageLabelNNR d
)NNd e
.NNe f

ShowDialogNNf p
(NNp q
)NNq r
;NNr s
ifOO 
(OO 
resultOO 
==OO 
trueOO 
)OO 
{PP 
NavigationServiceQQ !
?QQ! "
.QQ" #
NavigateQQ# +
(QQ+ ,
newQQ, /
GamePageQQ0 8
(QQ8 9
)QQ9 :
)QQ: ;
;QQ; <
LogoutCurrentUserRR !
(RR! "
)RR" #
;RR# $
}SS 
}TT 	
privateVV 
boolVV 
IsGuestVV 
(VV 
)VV 
{WW 	
returnXX 
stringXX 
.XX 
EqualsXX  
(XX  !
CurrentSessionXX! /
.XX/ 0
CurrentUserNicknameXX0 C
,XXC D
$strXXE L
,XXL M
StringComparisonXXN ^
.XX^ _
OrdinalIgnoreCaseXX_ p
)XXp q
;XXq r
}YY 	
private[[ 
void[[ 
LogoutCurrentUser[[ &
([[& '
)[[' (
{\\ 	
try]] 
{^^ 
if__ 
(__ 
IsGuest__ 
(__ 
)__ 
)__ 
{`` 
CurrentSessionaa "
.aa" #
CurrentUserNicknameaa# 6
=aa7 8
nullaa9 =
;aa= >
CurrentSessionbb "
.bb" #"
CurrentUserProfileDatabb# 9
=bb: ;
nullbb< @
;bb@ A
returncc 
;cc 
}dd 
ifff 
(ff 
!ff 
stringff 
.ff 
IsNullOrWhiteSpaceff .
(ff. /
CurrentSessionff/ =
.ff= >
CurrentUserNicknameff> Q
)ffQ R
)ffR S
{gg 
varhh 
contexthh 
=hh  !
newhh" %
InstanceContexthh& 5
(hh5 6
thishh6 :
)hh: ;
;hh; <
_logoutClientii !
=ii" #
newii$ '
LogoutManagerClientii( ;
(ii; <
contextii< C
)iiC D
;iiD E
_logoutClientjj !
.jj! "
LogoutAsyncjj" -
(jj- .
CurrentSessionjj. <
.jj< =
CurrentUserNicknamejj= P
)jjP Q
;jjQ R
CurrentSessionll "
.ll" #
CurrentUserNicknamell# 6
=ll7 8
nullll9 =
;ll= >
CurrentSessionmm "
.mm" #"
CurrentUserProfileDatamm# 9
=mm: ;
nullmm< @
;mm@ A
}nn 
}oo 
catchpp 
(pp 
	Exceptionpp 
expp 
)pp  
{qq 
newrr 
SimplePopUpWindowrr %
(rr% &
Globalrr& ,
.rr, -
UnsuccessfulLabelrr- >
,rr> ?
exrr@ B
.rrB C
MessagerrC J
)rrJ K
.rrK L

ShowDialogrrL V
(rrV W
)rrW X
;rrX Y
}ss 
}tt 	
}uu 
}vv Ë
ÜC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\LeaderboardsPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
LeaderboardsPage )
:* +
Page, 0
{ 
public 
LeaderboardsPage 
(  
)  !
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
} 
} œ
~C:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\ShopPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
ShopPage !
:" #
Page$ (
{ 
public 
ShopPage 
( 
) 
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
} 
} ¿;
ÇC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\SettingsPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public		 

partial		 
class		 
SettingsPage		 %
:		& '
Page		( ,
{

 
private 
bool 
_initializing "
=# $
true% )
;) *
public 
SettingsPage 
( 
) 
{ 	
InitializeComponent 
(  
)  !
;! "
var 
saved 
= 

Properties "
." #
Settings# +
.+ ,
Default, 3
.3 4
languageCode4 @
;@ A
if 
( 
string 
. 
IsNullOrWhiteSpace )
() *
saved* /
)/ 0
)0 1
saved 
= 
$str 
;  
Thread 
. 
CurrentThread  
.  !
CurrentUICulture! 1
=2 3
new4 7
CultureInfo8 C
(C D
savedD I
)I J
;J K
Thread 
. 
CurrentThread  
.  !
CurrentCulture! /
=0 1
new2 5
CultureInfo6 A
(A B
savedB G
)G H
;H I 
SelectLanguageByCode  
(  !
saved! &
)& '
;' (
if 
( 

Properties 
. 
Settings #
.# $
Default$ +
.+ ,

lastVolume, 6
>7 8
$num9 :
): ;
VolumeSlider 
. 
Value "
=# $

Properties% /
./ 0
Settings0 8
.8 9
Default9 @
.@ A

lastVolumeA K
;K L
else 
VolumeSlider 
. 
Value "
=# $
$num% '
;' (
_initializing 
= 
false !
;! "
}   	
private"" 
void"" 
CloseButton_Click"" &
(""& '
object""' -
sender"". 4
,""4 5
RoutedEventArgs""6 E
e""F G
)""G H
{## 	
NavigationService$$ 
?$$ 
.$$ 
Navigate$$ '
($$' (
new$$( +
MainMenuPage$$, 8
($$8 9
)$$9 :
)$$: ;
;$$; <
}%% 	
private'' 
void''  
SelectLanguageByCode'' )
('') *
string''* 0
code''1 5
)''5 6
{(( 	
for)) 
()) 
int)) 
i)) 
=)) 
$num)) 
;)) 
i)) 
<)) 
LanguageComboBox))  0
.))0 1
Items))1 6
.))6 7
Count))7 <
;))< =
i))> ?
++))? A
)))A B
{** 
if++ 
(++ 
LanguageComboBox++ $
.++$ %
Items++% *
[++* +
i+++ ,
]++, -
is++. 0
ComboBoxItem++1 =
item++> B
&&++C E
item,, 
.,, 
Tag,, 
is,, 
string,,  &
tag,,' *
&&,,+ -
tag-- 
==-- 
code-- 
)--  
{.. 
LanguageComboBox// $
.//$ %
SelectedIndex//% 2
=//3 4
i//5 6
;//6 7
return00 
;00 
}11 
}22 
LanguageComboBox33 
.33 
SelectedIndex33 *
=33+ ,
$num33- .
;33. /
}44 	
private66 
void66 -
!LanguageComboBox_SelectionChanged66 6
(666 7
object667 =
sender66> D
,66D E%
SelectionChangedEventArgs66F _
e66` a
)66a b
{77 	
if88 
(88 
_initializing88 
)88 
return88 %
;88% &
if:: 
(:: 
LanguageComboBox::  
.::  !
SelectedItem::! -
is::. 0
ComboBoxItem::1 =
item::> B
&&::C E
item;; 
.;; 
Tag;; 
is;; 
string;; "
newLangCode;;# .
);;. /
{<< 
if== 
(== 

Properties== 
.== 
Settings== '
.==' (
Default==( /
.==/ 0
languageCode==0 <
===== ?
newLangCode==@ K
)==K L
return==M S
;==S T

Properties?? 
.?? 
Settings?? #
.??# $
Default??$ +
.??+ ,
languageCode??, 8
=??9 :
newLangCode??; F
;??F G

Properties@@ 
.@@ 
Settings@@ #
.@@# $
Default@@$ +
.@@+ ,
Save@@, 0
(@@0 1
)@@1 2
;@@2 3
ThreadBB 
.BB 
CurrentThreadBB $
.BB$ %
CurrentUICultureBB% 5
=BB6 7
newBB8 ;
CultureInfoBB< G
(BBG H
newLangCodeBBH S
)BBS T
;BBT U
ThreadCC 
.CC 
CurrentThreadCC $
.CC$ %
CurrentCultureCC% 3
=CC4 5
newCC6 9
CultureInfoCC: E
(CCE F
newLangCodeCCF Q
)CCQ R
;CCR S

DispatcherEE 
.EE 
BeginInvokeEE &
(EE& '
newEE' *
SystemEE+ 1
.EE1 2
ActionEE2 8
(EE8 9
(EE9 :
)EE: ;
=>EE< >
{FF 
NavigationServiceGG %
?GG% &
.GG& '
NavigateGG' /
(GG/ 0
newGG0 3
SettingsPageGG4 @
(GG@ A
)GGA B
)GGB C
;GGC D
}HH 
)HH 
,HH 
DispatcherPriorityHH &
.HH& '
ApplicationIdleHH' 6
)HH6 7
;HH7 8
}II 
}JJ 	
privateLL 
voidLL %
VolumeSlider_ValueChangedLL .
(LL. /
objectLL/ 5
senderLL6 <
,LL< =*
RoutedPropertyChangedEventArgsLL> \
<LL\ ]
doubleLL] c
>LLc d
eLLe f
)LLf g
{MM 	
ifNN 
(NN 
_initializingNN 
)NN 
returnNN %
;NN% &
varPP 

mainWindowPP 
=PP 
ApplicationPP (
.PP( )
CurrentPP) 0
.PP0 1

MainWindowPP1 ;
asPP< >
UnoLisClientPP? K
.PPK L
UIPPL N
.PPN O

MainWindowPPO Y
;PPY Z

mainWindowQQ 
?QQ 
.QQ 
SetMusicVolumeQQ &
(QQ& '
eQQ' (
.QQ( )
NewValueQQ) 1
)QQ1 2
;QQ2 3

PropertiesTT 
.TT 
SettingsTT 
.TT  
DefaultTT  '
.TT' (

lastVolumeTT( 2
=TT3 4
eTT5 6
.TT6 7
NewValueTT7 ?
;TT? @

PropertiesUU 
.UU 
SettingsUU 
.UU  
DefaultUU  '
.UU' (
SaveUU( ,
(UU, -
)UU- .
;UU. /
}VV 	
}XX 
}YY Å
~C:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\GamePage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
GamePage !
:" #
Page$ (
{ 
public 
GamePage 
( 
) 
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
private 
void !
PlayGuestButton_Click *
(* +
object+ 1
sender2 8
,8 9
RoutedEventArgs: I
eJ K
)K L
{ 	
SoundManager   
.   
	PlayClick   "
(  " #
)  # $
;  $ %
CurrentSession!! 
.!! 
CurrentUserNickname!! .
=!!/ 0
$str!!1 8
;!!8 9
NavigationService"" 
?"" 
."" 
Navigate"" '
(""' (
new""( +
MainMenuPage"", 8
(""8 9
)""9 :
)"": ;
;""; <
}## 	
private%% 
void%% 
LoginButtonClick%% %
(%%% &
object%%& ,
sender%%- 3
,%%3 4
RoutedEventArgs%%5 D
e%%E F
)%%F G
{&& 	
SoundManager'' 
.'' 
	PlayClick'' "
(''" #
)''# $
;''$ %
NavigationService(( 
?(( 
.(( 
Navigate(( '
(((' (
new((( +
	LoginPage((, 5
(((5 6
)((6 7
)((7 8
;((8 9
})) 	
}** 
}++ ¢
ÑC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\AvatarShopPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
AvatarShopPage '
:( )
Page* .
{ 
public 
AvatarShopPage 
( 
) 
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
private 
void 
BackButton_Click %
(% &
object& ,
sender- 3
,3 4
RoutedEventArgs5 D
eE F
)F G
{ 	
SoundManager 
. 
	PlayClick "
(" #
)# $
;$ %
NavigationService 
? 
. 
GoBack %
(% &
)& '
;' (
} 	
private!! 
void!! 
BuySpecial_Click!! %
(!!% &
object!!& ,
sender!!- 3
,!!3 4
RoutedEventArgs!!5 D
e!!E F
)!!F G
{"" 	
SoundManager## 
.## 
	PlayClick## "
(##" #
)### $
;##$ %
new$$ 
SimplePopUpWindow$$ !
($$! "
$str$$" 7
,$$7 8
$str$$9 T
)$$T U
.$$U V

ShowDialog$$V `
($$` a
)$$a b
;$$b c
}%% 	
private'' 
void'' 
BuyEpic_Click'' "
(''" #
object''# )
sender''* 0
,''0 1
RoutedEventArgs''2 A
e''B C
)''C D
{(( 	
SoundManager)) 
.)) 
	PlayClick)) "
())" #
)))# $
;))$ %
new** 
SimplePopUpWindow** !
(**! "
$str**" 7
,**7 8
$str**9 R
)**R S
.**S T

ShowDialog**T ^
(**^ _
)**_ `
;**` a
}++ 	
private-- 
void-- 
BuyLegendary_Click-- '
(--' (
object--( .
sender--/ 5
,--5 6
RoutedEventArgs--7 F
e--G H
)--H I
{.. 	
SoundManager// 
.// 
	PlayClick// "
(//" #
)//# $
;//$ %
new00 
SimplePopUpWindow00 !
(00! "
$str00" 7
,007 8
$str009 V
)00V W
.00W X

ShowDialog00X b
(00b c
)00c d
;00d e
}11 	
}22 
}33 €
ÜC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\GameSettingsPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
GameSettingsPage )
:* +
Page, 0
{ 
public 
GameSettingsPage 
(  
)  !
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
private 
void 
CloseButton_Click &
(& '
object' -
sender. 4
,4 5
RoutedEventArgs6 E
eF G
)G H
{ 	
SoundManager   
.   
	PlayClick   "
(  " #
)  # $
;  $ %
NavigationService!! 
?!! 
.!! 
GoBack!! %
(!!% &
)!!& '
;!!' (
}"" 	
private$$ 
void$$ "
CreateGameButton_Click$$ +
($$+ ,
object$$, 2
sender$$3 9
,$$9 :
RoutedEventArgs$$; J
e$$K L
)$$L M
{%% 	
SoundManager&& 
.&& 
	PlayClick&& "
(&&" #
)&&# $
;&&$ %
new'' 
SimplePopUpWindow'' !
(''! "
$str''" 0
,''0 1
$str''2 \
)''\ ]
.''] ^

ShowDialog''^ h
(''h i
)''i j
;''j k
NavigationService(( 
?(( 
.(( 
Navigate(( '
(((' (
new((( +
MatchLobbyPage((, :
(((: ;
)((; <
)((< =
;((= >
})) 	
private** 
void** 
MusicToggle_Checked** (
(**( )
object**) /
sender**0 6
,**6 7
RoutedEventArgs**8 G
e**H I
)**I J
{++ 	
if,, 
(,, 
MusicToggle,, 
.,, 
Content,, #
is,,$ &
	TextBlock,,' 0
tb,,1 3
),,3 4
tb-- 
.-- 
Text-- 
=-- 
$str-- 
;-- 
}// 	
private11 
void11 !
MusicToggle_Unchecked11 *
(11* +
object11+ 1
sender112 8
,118 9
RoutedEventArgs11: I
e11J K
)11K L
{22 	
if33 
(33 
MusicToggle33 
.33 
Content33 #
is33$ &
	TextBlock33' 0
tb331 3
)333 4
tb44 
.44 
Text44 
=44 
$str44 
;44 
}66 	
private88 
void88 
SoundToggle_Checked88 (
(88( )
object88) /
sender880 6
,886 7
RoutedEventArgs888 G
e88H I
)88I J
{99 	
if:: 
(:: 
SoundToggle:: 
.:: 
Content:: #
is::$ &
	TextBlock::' 0
tb::1 3
)::3 4
tb;; 
.;; 
Text;; 
=;; 
$str;; 
;;; 
}== 	
private?? 
void?? !
SoundToggle_Unchecked?? *
(??* +
object??+ 1
sender??2 8
,??8 9
RoutedEventArgs??: I
e??J K
)??K L
{@@ 	
ifAA 
(AA 
SoundToggleAA 
.AA 
ContentAA #
isAA$ &
	TextBlockAA' 0
tbAA1 3
)AA3 4
tbBB 
.BB 
TextBB 
=BB 
$strBB 
;BB 
}DD 	
}EE 
}FF ©6
ÇC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\RegisterPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
RegisterPage %
:& '
Page( ,
,, -$
IRegisterManagerCallback. F
{ 
private !
RegisterManagerClient %
_registerClient& 5
;5 6
private 
LoadingPopUpWindow "
_loadingPopUpWindow# 6
;6 7
public!! 
RegisterPage!! 
(!! 
)!! 
{"" 	
InitializeComponent## 
(##  
)##  !
;##! "
}$$ 	
public&& 
void&& 
RegisterResponse&& $
(&&$ %
bool&&% )
success&&* 1
,&&1 2
string&&3 9
message&&: A
)&&A B
{'' 	
Application(( 
.(( 
Current(( 
.((  

Dispatcher((  *
.((* +
Invoke((+ 1
(((1 2
(((2 3
)((3 4
=>((5 7
{)) 
_loadingPopUpWindow** #
?**# $
.**$ %
StopLoadingAndClose**% 8
(**8 9
)**9 :
;**: ;
if++ 
(++ 
success++ 
)++ 
{,, 
new-- 
SimplePopUpWindow-- )
(--) *
Global--* 0
.--0 1
SuccessLabel--1 =
,--= >
message--? F
)--F G
.--G H

ShowDialog--H R
(--R S
)--S T
;--T U
NavigationService.. %
?..% &
...& '
Navigate..' /
(../ 0
new..0 3
	LoginPage..4 =
(..= >
)..> ?
)..? @
;..@ A
}// 
else00 
{11 
new22 
SimplePopUpWindow22 )
(22) *
Global22* 0
.220 1
UnsuccessfulLabel221 B
,22B C
message22D K
)22K L
.22L M

ShowDialog22M W
(22W X
)22X Y
;22Y Z
}33 
}44 
)44 
;44 
}55 	
private77 
void77 
ClickCancelButton77 &
(77& '
object77' -
sender77. 4
,774 5
RoutedEventArgs776 E
e77F G
)77G H
{88 	
SoundManager99 
.99 
	PlayClick99 "
(99" #
)99# $
;99$ %
NavigationService:: 
?:: 
.:: 
GoBack:: %
(::% &
)::& '
;::' (
};; 	
private== 
void== 
ClickSignInButton== &
(==& '
object==' -
sender==. 4
,==4 5
RoutedEventArgs==6 E
e==F G
)==G H
{>> 	
SoundManager?? 
.?? 
	PlayClick?? "
(??" #
)??# $
;??$ %
string@@ 
nickname@@ 
=@@ 
NicknameTextBox@@ -
.@@- .
Text@@. 2
.@@2 3
Trim@@3 7
(@@7 8
)@@8 9
;@@9 :
stringAA 
fullnameAA 
=AA 
FullNameTextBoxAA -
.AA- .
TextAA. 2
.AA2 3
TrimAA3 7
(AA7 8
)AA8 9
;AA9 :
stringBB 
emailBB 
=BB 
EmailTextBoxBB '
.BB' (
TextBB( ,
.BB, -
TrimBB- 1
(BB1 2
)BB2 3
;BB3 4
stringCC 
passwordCC 
=CC 
PasswordFieldCC +
.CC+ ,
PasswordCC, 4
;CC4 5
stringDD 
rewritedPasswordDD #
=DD$ %!
RewritedPasswordFieldDD& ;
.DD; <
PasswordDD< D
;DDD E
varFF 
registrationDataFF  
=FF! "
newFF# &
RegistrationDataFF' 7
{GG 
NicknameHH 
=HH 
nicknameHH #
,HH# $
FullNameII 
=II 
fullnameII #
,II# $
EmailJJ 
=JJ 
emailJJ 
,JJ 
PasswordKK 
=KK 
passwordKK #
}LL 
;LL 
ListNN 
<NN 
stringNN 
>NN 
errorsNN 
=NN  !
UserValidatorNN" /
.NN/ 0 
ValidateRegistrationNN0 D
(NND E
registrationDataNNE U
,NNU V
rewritedPasswordNNW g
)NNg h
;NNh i
ifOO 
(OO 
errorsOO 
.OO 
CountOO 
>OO 
$numOO  
)OO  !
{PP 
stringQQ 
messageQQ 
=QQ  
$strQQ! %
+QQ& '
stringQQ( .
.QQ. /
JoinQQ/ 3
(QQ3 4
$strQQ4 :
,QQ: ;
errorsQQ< B
)QQB C
;QQC D
newRR 
SimplePopUpWindowRR %
(RR% &
GlobalRR& ,
.RR, -
UnsuccessfulLabelRR- >
,RR> ?
messageRR@ G
)RRG H
.RRH I

ShowDialogRRI S
(RRS T
)RRT U
;RRU V
returnSS 
;SS 
}TT 
tryVV 
{WW 
_loadingPopUpWindowXX #
=XX$ %
newXX& )
LoadingPopUpWindowXX* <
(XX< =
)XX= >
{YY 
OwnerZZ 
=ZZ 
WindowZZ "
.ZZ" #
	GetWindowZZ# ,
(ZZ, -
thisZZ- 1
)ZZ1 2
}[[ 
;[[ 
_loadingPopUpWindow\\ #
.\\# $
Show\\$ (
(\\( )
)\\) *
;\\* +
var]] 
context]] 
=]] 
new]] !
InstanceContext]]" 1
(]]1 2
this]]2 6
)]]6 7
;]]7 8
_registerClient^^ 
=^^  !
new^^" %!
RegisterManagerClient^^& ;
(^^; <
context^^< C
)^^C D
;^^D E
_registerClient__ 
.__  
Register__  (
(__( )
registrationData__) 9
)__9 :
;__: ;
}`` 
catchaa 
(aa 
	Exceptionaa 
)aa 
{bb 
_loadingPopUpWindowcc #
?cc# $
.cc$ %
StopLoadingAndClosecc% 8
(cc8 9
)cc9 :
;cc: ;
newdd 
SimplePopUpWindowdd %
(dd% &
Globaldd& ,
.dd, -
UnsuccessfulLabeldd- >
,dd> ?
ErrorMessagesdd@ M
.ddM N'
ConnectionErrorMessageLabelddN i
)ddi j
.ddj k

ShowDialogddk u
(ddu v
)ddv w
;ddw x
}ee 
}ff 	
}gg 
}hh Â
ÖC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\DropChancesPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
DropChancesPage (
:) *
Page+ /
{ 
public 
DropChancesPage 
( 
)  
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
} 
} •

âC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\AvatarSelectionPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
AvatarSelectionPage ,
:- .
Page/ 3
{ 
public 
AvatarSelectionPage "
(" #
)# $
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
private 
void 

Save_Click 
(  
object  &
sender' -
,- .
RoutedEventArgs/ >
e? @
)@ A
{ 	

MessageBox 
. 
Show 
( 
$str 8
)8 9
;9 :
} 	
private 
void 
Cancel_Click !
(! "
object" (
sender) /
,/ 0
RoutedEventArgs1 @
eA B
)B C
{ 	
NavigationService   
?   
.   
GoBack   %
(  % &
)  & '
;  ' (
}!! 	
}"" 
}## ã
ÇC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Managers\CurrentSession.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Managers "
{		 
public

 

static

 
class

 
CurrentSession

 &
{ 
public 
static 
string 
CurrentUserNickname 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
public 
static 
ClientProfileData '"
CurrentUserProfileData( >
{? @
getA D
;D E
setF I
;I J
}K L
} 
} ﬂ
ÉC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\MyFriendsPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
MyFriendsPage &
:' (
Page) -
{ 
public 
MyFriendsPage 
( 
) 
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
} 
} ﬁ
ÖC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Managers\ClientProfileData.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Managers "
{ 
public		 

class		 
ClientProfileData		 "
{

 
public 
string 
Nickname 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
FullName 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
int 
ExperiencePoints #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
int 
MatchesPlayed  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 
Wins 
{ 
get 
; 
set "
;" #
}$ %
public 
int 
Losses 
{ 
get 
;  
set! $
;$ %
}& '
public 
int 
Streak 
{ 
get 
;  
set! $
;$ %
}& '
public 
int 
	MaxStreak 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
FacebookUrl !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
string 
InstagramUrl "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
	TikTokUrl 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} Ë
ÜC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\MatchResultsPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
MatchResultsPage )
:* +
Page, 0
{ 
public 
MatchResultsPage 
(  
)  !
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
} 
} ∆Õ
ÑC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\MatchLobbyPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

class 
Friend 
{ 
public 
string 

FriendName  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
string 
Status 
{ 
get "
;" #
set$ '
;' (
}) *
public 
Brush 
StatusColor  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
bool 
Invited 
{ 
get !
;! "
set# &
;& '
}( )
} 
public 

partial 
class 
MatchLobbyPage '
:( )
Page* .
{ 
public  
ObservableCollection #
<# $
Friend$ *
>* +
Friends, 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
=B C
newD G 
ObservableCollectionH \
<\ ]
Friend] c
>c d
(d e
)e f
;f g
private 
bool 
_isChatVisible #
=$ %
false& +
;+ ,
private 
Grid 
_settingsOverlay %
;% &
public 
MatchLobbyPage 
( 
) 
{ 	
InitializeComponent 
(  
)  !
;! "
LoadFriends 
( 
) 
; 
var"" 

mainWindow"" 
="" 
Application"" (
.""( )
Current"") 0
.""0 1

MainWindow""1 ;
as""< >
UnoLisClient""? K
.""K L
UI""L N
.""N O

MainWindow""O Y
;""Y Z

mainWindow## 
?## 
.## 
SetBackgroundMedia## *
(##* +
$str##+ B
,##B C
$str##D [
)##[ \
;##\ ]
}$$ 	
private'' 
void'' 
LoadFriends''  
(''  !
)''! "
{(( 	
Friends)) 
.)) 
Add)) 
()) 
new)) 
Friend)) "
{))# $

FriendName))% /
=))0 1
$str))2 ?
,))? @
Status))A G
=))H I
$str))J R
,))R S
StatusColor))T _
=))` a
Brushes))b i
.))i j
Lime))j n
}))o p
)))p q
;))q r
Friends** 
.** 
Add** 
(** 
new** 
Friend** "
{**# $

FriendName**% /
=**0 1
$str**2 ;
,**; <
Status**= C
=**D E
$str**F O
,**O P
StatusColor**Q \
=**] ^
Brushes**_ f
.**f g
Gray**g k
}**l m
)**m n
;**n o
Friends++ 
.++ 
Add++ 
(++ 
new++ 
Friend++ "
{++# $

FriendName++% /
=++0 1
$str++2 <
,++< =
Status++> D
=++E F
$str++G O
,++O P
StatusColor++Q \
=++] ^
Brushes++_ f
.++f g
Lime++g k
}++l m
)++m n
;++n o
Friends,, 
.,, 
Add,, 
(,, 
new,, 
Friend,, "
{,,# $

FriendName,,% /
=,,0 1
$str,,2 ?
,,,? @
Status,,A G
=,,H I
$str,,J R
,,,R S
StatusColor,,T _
=,,` a
Brushes,,b i
.,,i j
Lime,,j n
},,o p
),,p q
;,,q r
FriendsList-- 
.-- 
ItemsSource-- #
=--$ %
Friends--& -
;--- .
}.. 	
private11 
void11 
InviteButton_Click11 '
(11' (
object11( .
sender11/ 5
,115 6
RoutedEventArgs117 F
e11G H
)11H I
{22 	
SoundManager33 
.33 
	PlayClick33 "
(33" #
)33# $
;33$ %
if44 
(44 
sender44 
is44 
Button44  
button44! '
&&44( *
button44+ 1
.441 2
DataContext442 =
is44> @
Friend44A G
friend44H N
)44N O
{55 
friend66 
.66 
Invited66 
=66  
!66! "
friend66" (
.66( )
Invited66) 0
;660 1
button77 
.77 
Content77 
=77  
friend77! '
.77' (
Invited77( /
?770 1
$str772 ;
:77< =
$str77> F
;77F G
button88 
.88 

Background88 !
=88" #
friend88$ *
.88* +
Invited88+ 2
?883 4
Brushes885 <
.88< =
Green88= B
:88C D
Brushes88E L
.88L M
Transparent88M X
;88X Y
}99 
}:: 	
private== 
void== #
SendInvitesButton_Click== ,
(==, -
object==- 3
sender==4 :
,==: ;
RoutedEventArgs==< K
e==L M
)==M N
{>> 	
SoundManager?? 
.?? 
	PlayClick?? "
(??" #
)??# $
;??$ %
var@@ 
invited@@ 
=@@ 
Friends@@ !
.@@! "
Where@@" '
(@@' (
f@@( )
=>@@* ,
f@@- .
.@@. /
Invited@@/ 6
)@@6 7
.@@7 8
Select@@8 >
(@@> ?
f@@? @
=>@@A C
f@@D E
.@@E F

FriendName@@F P
)@@P Q
.@@Q R
ToList@@R X
(@@X Y
)@@Y Z
;@@Z [
ifBB 
(BB 
invitedBB 
.BB 
AnyBB 
(BB 
)BB 
)BB 
{CC 

MessageBoxDD 
.DD 
ShowDD 
(DD  
$"DD  "
$strDD" 7
{DD7 8
stringDD8 >
.DD> ?
JoinDD? C
(DDC D
$strDDD H
,DDH I
invitedDDJ Q
)DDQ R
}DDR S
"DDS T
,DDT U
$strDDV _
,DD_ `
MessageBoxButtonDDa q
.DDq r
OKDDr t
,DDt u
MessageBoxImage	DDv Ö
.
DDÖ Ü
Information
DDÜ ë
)
DDë í
;
DDí ì
}EE 
elseFF 
{GG 

MessageBoxHH 
.HH 
ShowHH 
(HH  
$strHH  E
,HHE F
$strHHG P
,HHP Q
MessageBoxButtonHHR b
.HHb c
OKHHc e
,HHe f
MessageBoxImageHHg v
.HHv w
WarningHHw ~
)HH~ 
;	HH Ä
}II 
}JJ 	
privateMM 
voidMM 
ChatButton_ClickMM %
(MM% &
objectMM& ,
senderMM- 3
,MM3 4
RoutedEventArgsMM5 D
eMME F
)MMF G
{NN 	
SoundManagerOO 
.OO 
	PlayClickOO "
(OO" #
)OO# $
;OO$ %
ifPP 
(PP 
_isChatVisiblePP 
)PP 
{QQ 
FadeOutRR 
(RR 
	ChatPopupRR !
)RR! "
;RR" #
_isChatVisibleSS 
=SS  
falseSS! &
;SS& '
}TT 
elseUU 
{VV 
FadeInWW 
(WW 
	ChatPopupWW  
)WW  !
;WW! "
_isChatVisibleXX 
=XX  
trueXX! %
;XX% &
}YY 
}ZZ 	
private]] 
void]]  
SettingsButton_Click]] )
(]]) *
object]]* 0
sender]]1 7
,]]7 8
RoutedEventArgs]]9 H
e]]I J
)]]J K
{^^ 	
SoundManager__ 
.__ 
	PlayClick__ "
(__" #
)__# $
;__$ %
if`` 
(`` 
_settingsOverlay``  
!=``! #
null``$ (
)``( )
returnaa 
;aa 
ShowSettingsModalcc 
(cc 
)cc 
;cc  
}dd 	
privategg 
voidgg 
FadeIngg 
(gg 
	UIElementgg %
elementgg& -
,gg- .
doublegg/ 5
durationgg6 >
=gg? @
$numggA D
)ggD E
{hh 	
elementii 
.ii 

Visibilityii 
=ii  

Visibilityii! +
.ii+ ,
Visibleii, 3
;ii3 4
elementjj 
.jj 
Opacityjj 
=jj 
$numjj 
;jj  
varll 
fadell 
=ll 
newll 
DoubleAnimationll *
{mm 
Fromnn 
=nn 
$numnn 
,nn 
Tooo 
=oo 
$numoo 
,oo 
Durationpp 
=pp 
TimeSpanpp #
.pp# $
FromSecondspp$ /
(pp/ 0
durationpp0 8
)pp8 9
,pp9 :
EasingFunctionqq 
=qq  
newqq! $
	CubicEaseqq% .
{qq/ 0

EasingModeqq1 ;
=qq< =

EasingModeqq> H
.qqH I
EaseOutqqI P
}qqQ R
}rr 
;rr 
elementtt 
.tt 
BeginAnimationtt "
(tt" #
	UIElementtt# ,
.tt, -
OpacityPropertytt- <
,tt< =
fadett> B
)ttB C
;ttC D
}uu 	
privatexx 
asyncxx 
voidxx 
FadeOutxx "
(xx" #
	UIElementxx# ,
elementxx- 4
,xx4 5
doublexx6 <
durationxx= E
=xxF G
$numxxH L
)xxL M
{yy 	
varzz 
fadezz 
=zz 
newzz 
DoubleAnimationzz *
{{{ 
From|| 
=|| 
$num|| 
,|| 
To}} 
=}} 
$num}} 
,}} 
Duration~~ 
=~~ 
TimeSpan~~ #
.~~# $
FromSeconds~~$ /
(~~/ 0
duration~~0 8
)~~8 9
,~~9 :
EasingFunction 
=  
new! $
	CubicEase% .
{/ 0

EasingMode1 ;
=< =

EasingMode> H
.H I
EaseInI O
}P Q
}
ÄÄ 
;
ÄÄ 
element
ÇÇ 
.
ÇÇ 
BeginAnimation
ÇÇ "
(
ÇÇ" #
	UIElement
ÇÇ# ,
.
ÇÇ, -
OpacityProperty
ÇÇ- <
,
ÇÇ< =
fade
ÇÇ> B
)
ÇÇB C
;
ÇÇC D
await
ÉÉ 
Task
ÉÉ 
.
ÉÉ 
Delay
ÉÉ 
(
ÉÉ 
(
ÉÉ 
int
ÉÉ !
)
ÉÉ! "
(
ÉÉ" #
duration
ÉÉ# +
*
ÉÉ, -
$num
ÉÉ. 2
)
ÉÉ2 3
)
ÉÉ3 4
;
ÉÉ4 5
element
ÑÑ 
.
ÑÑ 

Visibility
ÑÑ 
=
ÑÑ  

Visibility
ÑÑ! +
.
ÑÑ+ ,
	Collapsed
ÑÑ, 5
;
ÑÑ5 6
}
ÖÖ 	
private
àà 
void
àà 
ShowSettingsModal
àà &
(
àà& '
)
àà' (
{
ââ 	
_settingsOverlay
ää 
=
ää 
new
ää "
Grid
ää# '
{
ãã 

Background
åå 
=
åå 
new
åå  
SolidColorBrush
åå! 0
(
åå0 1
Color
åå1 6
.
åå6 7
FromArgb
åå7 ?
(
åå? @
$num
åå@ C
,
ååC D
$num
ååE F
,
ååF G
$num
ååH I
,
ååI J
$num
ååK L
)
ååL M
)
ååM N
,
ååN O!
HorizontalAlignment
çç #
=
çç$ %!
HorizontalAlignment
çç& 9
.
çç9 :
Stretch
çç: A
,
ççA B
VerticalAlignment
éé !
=
éé" #
VerticalAlignment
éé$ 5
.
éé5 6
Stretch
éé6 =
}
èè 
;
èè 
var
ëë 
border
ëë 
=
ëë 
new
ëë 
Border
ëë #
{
íí 

Background
ìì 
=
ìì 
new
ìì  
SolidColorBrush
ìì! 0
(
ìì0 1
Color
ìì1 6
.
ìì6 7
FromArgb
ìì7 ?
(
ìì? @
$num
ìì@ C
,
ììC D
$num
ììE G
,
ììG H
$num
ììI K
,
ììK L
$num
ììM O
)
ììO P
)
ììP Q
,
ììQ R
CornerRadius
îî 
=
îî 
new
îî "
CornerRadius
îî# /
(
îî/ 0
$num
îî0 2
)
îî2 3
,
îî3 4
Width
ïï 
=
ïï 
$num
ïï 
,
ïï 
Height
ññ 
=
ññ 
$num
ññ 
,
ññ 
Padding
óó 
=
óó 
new
óó 
	Thickness
óó '
(
óó' (
$num
óó( *
)
óó* +
,
óó+ ,!
HorizontalAlignment
òò #
=
òò$ %!
HorizontalAlignment
òò& 9
.
òò9 :
Center
òò: @
,
òò@ A
VerticalAlignment
ôô !
=
ôô" #
VerticalAlignment
ôô$ 5
.
ôô5 6
Center
ôô6 <
}
öö 
;
öö 
var
úú 
stack
úú 
=
úú 
new
úú 

StackPanel
úú &
{
ùù 
VerticalAlignment
ûû !
=
ûû" #
VerticalAlignment
ûû$ 5
.
ûû5 6
Center
ûû6 <
,
ûû< =!
HorizontalAlignment
üü #
=
üü$ %!
HorizontalAlignment
üü& 9
.
üü9 :
Center
üü: @
}
†† 
;
†† 
var
¢¢ 
title
¢¢ 
=
¢¢ 
new
¢¢ 
	TextBlock
¢¢ %
{
££ 
Text
§§ 
=
§§ 
$str
§§ !
,
§§! "
FontSize
•• 
=
•• 
$num
•• 
,
•• 

FontWeight
¶¶ 
=
¶¶ 
FontWeights
¶¶ (
.
¶¶( )
Bold
¶¶) -
,
¶¶- .

Foreground
ßß 
=
ßß 
Brushes
ßß $
.
ßß$ %
White
ßß% *
,
ßß* +!
HorizontalAlignment
®® #
=
®®$ %!
HorizontalAlignment
®®& 9
.
®®9 :
Center
®®: @
,
®®@ A
Margin
©© 
=
©© 
new
©© 
	Thickness
©© &
(
©©& '
$num
©©' (
,
©©( )
$num
©©* +
,
©©+ ,
$num
©©- .
,
©©. /
$num
©©0 2
)
©©2 3
}
™™ 
;
™™ 
var
¨¨ 
volumeLabel
¨¨ 
=
¨¨ 
new
¨¨ !
	TextBlock
¨¨" +
{
≠≠ 
Text
ÆÆ 
=
ÆÆ 
$str
ÆÆ 
,
ÆÆ  
FontSize
ØØ 
=
ØØ 
$num
ØØ 
,
ØØ 

Foreground
∞∞ 
=
∞∞ 
Brushes
∞∞ $
.
∞∞$ %
White
∞∞% *
}
±± 
;
±± 
var
≥≥ 
volumeSlider
≥≥ 
=
≥≥ 
new
≥≥ "
Slider
≥≥# )
{
¥¥ 
Minimum
µµ 
=
µµ 
$num
µµ 
,
µµ 
Maximum
∂∂ 
=
∂∂ 
$num
∂∂ 
,
∂∂ 
Value
∑∑ 
=
∑∑ 
$num
∑∑ 
,
∑∑ 
Width
∏∏ 
=
∏∏ 
$num
∏∏ 
,
∏∏ 
Margin
ππ 
=
ππ 
new
ππ 
	Thickness
ππ &
(
ππ& '
$num
ππ' (
,
ππ( )
$num
ππ* ,
,
ππ, -
$num
ππ. /
,
ππ/ 0
$num
ππ1 3
)
ππ3 4
}
∫∫ 
;
∫∫ 
var
ºº 

exitButton
ºº 
=
ºº 
new
ºº  
Button
ºº! '
{
ΩΩ 
Content
ææ 
=
ææ 
$str
ææ '
,
ææ' (
Width
øø 
=
øø 
$num
øø 
,
øø 
Height
¿¿ 
=
¿¿ 
$num
¿¿ 
,
¿¿ 
Margin
¡¡ 
=
¡¡ 
new
¡¡ 
	Thickness
¡¡ &
(
¡¡& '
$num
¡¡' (
,
¡¡( )
$num
¡¡* ,
,
¡¡, -
$num
¡¡. /
,
¡¡/ 0
$num
¡¡1 2
)
¡¡2 3
,
¡¡3 4
Style
¬¬ 
=
¬¬ 
(
¬¬ 
Style
¬¬ 
)
¬¬ 
FindResource
¬¬ +
(
¬¬+ ,
$str
¬¬, B
)
¬¬B C
}
√√ 
;
√√ 

exitButton
ƒƒ 
.
ƒƒ 
Click
ƒƒ 
+=
ƒƒ 
async
ƒƒ  %
(
ƒƒ& '
s
ƒƒ' (
,
ƒƒ( )
e
ƒƒ* +
)
ƒƒ+ ,
=>
ƒƒ- /
{
≈≈ 
SoundManager
∆∆ 
.
∆∆ 
	PlayClick
∆∆ &
(
∆∆& '
)
∆∆' (
;
∆∆( )
var
…… 
questionPopup
…… !
=
……" #
new
……$ '!
QuestionPopUpWindow
……( ;
(
……; <
$str
……< E
,
……E F
$str
……G r
)
……r s
;
……s t
bool
   
?
   
result
   
=
   
questionPopup
   ,
.
  , -

ShowDialog
  - 7
(
  7 8
)
  8 9
;
  9 :
if
ÃÃ 
(
ÃÃ 
result
ÃÃ 
==
ÃÃ 
true
ÃÃ "
)
ÃÃ" #
{
ÕÕ 
var
ŒŒ 

mainWindow
ŒŒ "
=
ŒŒ# $
Application
ŒŒ% 0
.
ŒŒ0 1
Current
ŒŒ1 8
.
ŒŒ8 9

MainWindow
ŒŒ9 C
as
ŒŒD F
UnoLisClient
ŒŒG S
.
ŒŒS T
UI
ŒŒT V
.
ŒŒV W

MainWindow
ŒŒW a
;
ŒŒa b
if
œœ 
(
œœ 

mainWindow
œœ "
!=
œœ# %
null
œœ& *
)
œœ* +
{
–– 

mainWindow
““ "
.
““" #&
RestoreDefaultBackground
““# ;
(
““; <
)
““< =
;
““= >
}
”” 
await
÷÷ 
FadeOutTransition
÷÷ +
(
÷÷+ ,
)
÷÷, -
;
÷÷- . 
CloseSettingsModal
ŸŸ &
(
ŸŸ& '
)
ŸŸ' (
;
ŸŸ( )
NavigationService
‹‹ %
?
‹‹% &
.
‹‹& '
Navigate
‹‹' /
(
‹‹/ 0
new
‹‹0 3
UnoLisClient
‹‹4 @
.
‹‹@ A
UI
‹‹A C
.
‹‹C D
Pages
‹‹D I
.
‹‹I J
MainMenuPage
‹‹J V
(
‹‹V W
)
‹‹W X
)
‹‹X Y
;
‹‹Y Z
}
›› 
else
ﬁﬁ 
{
ﬂﬂ 
SoundManager
··  
.
··  !
	PlaySound
··! *
(
··* +
$str
··+ 7
,
··7 8
$num
··9 <
)
··< =
;
··= >
}
‚‚ 
}
„„ 
;
„„ 
var
ÊÊ 
closeButton
ÊÊ 
=
ÊÊ 
new
ÊÊ !
Button
ÊÊ" (
{
ÁÁ 
Content
ËË 
=
ËË 
$str
ËË !
,
ËË! "
Width
ÈÈ 
=
ÈÈ 
$num
ÈÈ 
,
ÈÈ 
Height
ÍÍ 
=
ÍÍ 
$num
ÍÍ 
,
ÍÍ 
Margin
ÎÎ 
=
ÎÎ 
new
ÎÎ 
	Thickness
ÎÎ &
(
ÎÎ& '
$num
ÎÎ' (
,
ÎÎ( )
$num
ÎÎ* ,
,
ÎÎ, -
$num
ÎÎ. /
,
ÎÎ/ 0
$num
ÎÎ1 2
)
ÎÎ2 3
,
ÎÎ3 4
Style
ÏÏ 
=
ÏÏ 
(
ÏÏ 
Style
ÏÏ 
)
ÏÏ 
FindResource
ÏÏ +
(
ÏÏ+ ,
$str
ÏÏ, @
)
ÏÏ@ A
}
ÌÌ 
;
ÌÌ 
closeButton
ÓÓ 
.
ÓÓ 
Click
ÓÓ 
+=
ÓÓ  
(
ÓÓ! "
s
ÓÓ" #
,
ÓÓ# $
e
ÓÓ% &
)
ÓÓ& '
=>
ÓÓ( * 
CloseSettingsModal
ÓÓ+ =
(
ÓÓ= >
)
ÓÓ> ?
;
ÓÓ? @
stack
 
.
 
Children
 
.
 
Add
 
(
 
title
 $
)
$ %
;
% &
stack
ÒÒ 
.
ÒÒ 
Children
ÒÒ 
.
ÒÒ 
Add
ÒÒ 
(
ÒÒ 
volumeLabel
ÒÒ *
)
ÒÒ* +
;
ÒÒ+ ,
stack
ÚÚ 
.
ÚÚ 
Children
ÚÚ 
.
ÚÚ 
Add
ÚÚ 
(
ÚÚ 
volumeSlider
ÚÚ +
)
ÚÚ+ ,
;
ÚÚ, -
stack
ÛÛ 
.
ÛÛ 
Children
ÛÛ 
.
ÛÛ 
Add
ÛÛ 
(
ÛÛ 

exitButton
ÛÛ )
)
ÛÛ) *
;
ÛÛ* +
stack
ÙÙ 
.
ÙÙ 
Children
ÙÙ 
.
ÙÙ 
Add
ÙÙ 
(
ÙÙ 
closeButton
ÙÙ *
)
ÙÙ* +
;
ÙÙ+ ,
border
ˆˆ 
.
ˆˆ 
Child
ˆˆ 
=
ˆˆ 
stack
ˆˆ  
;
ˆˆ  !
_settingsOverlay
˜˜ 
.
˜˜ 
Children
˜˜ %
.
˜˜% &
Add
˜˜& )
(
˜˜) *
border
˜˜* 0
)
˜˜0 1
;
˜˜1 2
var
˘˘ 
root
˘˘ 
=
˘˘ 
Window
˘˘ 
.
˘˘ 
	GetWindow
˘˘ '
(
˘˘' (
this
˘˘( ,
)
˘˘, -
?
˘˘- .
.
˘˘. /
Content
˘˘/ 6
as
˘˘7 9
Grid
˘˘: >
;
˘˘> ?
if
˙˙ 
(
˙˙ 
root
˙˙ 
!=
˙˙ 
null
˙˙ 
)
˙˙ 
root
˚˚ 
.
˚˚ 
Children
˚˚ 
.
˚˚ 
Add
˚˚ !
(
˚˚! "
_settingsOverlay
˚˚" 2
)
˚˚2 3
;
˚˚3 4
else
¸¸ 
if
¸¸ 
(
¸¸ 
this
¸¸ 
.
¸¸ 
Content
¸¸ !
is
¸¸" $
Grid
¸¸% )
grid
¸¸* .
)
¸¸. /
grid
˝˝ 
.
˝˝ 
Children
˝˝ 
.
˝˝ 
Add
˝˝ !
(
˝˝! "
_settingsOverlay
˝˝" 2
)
˝˝2 3
;
˝˝3 4
FadeIn
ˇˇ 
(
ˇˇ 
_settingsOverlay
ˇˇ #
,
ˇˇ# $
$num
ˇˇ% (
)
ˇˇ( )
;
ˇˇ) *
}
ÄÄ 	
private
ÉÉ 
async
ÉÉ 
void
ÉÉ  
CloseSettingsModal
ÉÉ -
(
ÉÉ- .
)
ÉÉ. /
{
ÑÑ 	
SoundManager
ÖÖ 
.
ÖÖ 
	PlayClick
ÖÖ "
(
ÖÖ" #
)
ÖÖ# $
;
ÖÖ$ %
if
ÜÜ 
(
ÜÜ 
_settingsOverlay
ÜÜ  
==
ÜÜ! #
null
ÜÜ$ (
)
ÜÜ( )
return
áá 
;
áá 
FadeOut
ââ 
(
ââ 
_settingsOverlay
ââ $
,
ââ$ %
$num
ââ& *
)
ââ* +
;
ââ+ ,
await
ää 
Task
ää 
.
ää 
Delay
ää 
(
ää 
$num
ää  
)
ää  !
;
ää! "
if
åå 
(
åå 
this
åå 
.
åå 
Content
åå 
is
åå 
Grid
åå  $
grid
åå% )
&&
åå* ,
grid
åå- 1
.
åå1 2
Children
åå2 :
.
åå: ;
Contains
åå; C
(
ååC D
_settingsOverlay
ååD T
)
ååT U
)
ååU V
grid
çç 
.
çç 
Children
çç 
.
çç 
Remove
çç $
(
çç$ %
_settingsOverlay
çç% 5
)
çç5 6
;
çç6 7
_settingsOverlay
èè 
=
èè 
null
èè #
;
èè# $
}
êê 	
private
íí 
async
íí 
Task
íí 
FadeOutTransition
íí ,
(
íí, -
)
íí- .
{
ìì 	
var
îî 
grid
îî 
=
îî 
this
îî 
.
îî 
Content
îî #
as
îî$ &
Grid
îî' +
;
îî+ ,
if
ïï 
(
ïï 
grid
ïï 
==
ïï 
null
ïï 
)
ïï 
return
ïï $
;
ïï$ %
var
óó 
fadeOut
óó 
=
óó 
new
óó 
System
óó $
.
óó$ %
Windows
óó% ,
.
óó, -
Media
óó- 2
.
óó2 3
	Animation
óó3 <
.
óó< =
DoubleAnimation
óó= L
{
òò 
From
ôô 
=
ôô 
$num
ôô 
,
ôô 
To
öö 
=
öö 
$num
öö 
,
öö 
Duration
õõ 
=
õõ 
TimeSpan
õõ #
.
õõ# $
FromSeconds
õõ$ /
(
õõ/ 0
$num
õõ0 3
)
õõ3 4
,
õõ4 5
EasingFunction
úú 
=
úú  
new
úú! $
System
úú% +
.
úú+ ,
Windows
úú, 3
.
úú3 4
Media
úú4 9
.
úú9 :
	Animation
úú: C
.
úúC D
	CubicEase
úúD M
{
ùù 

EasingMode
ûû 
=
ûû  
System
ûû! '
.
ûû' (
Windows
ûû( /
.
ûû/ 0
Media
ûû0 5
.
ûû5 6
	Animation
ûû6 ?
.
ûû? @

EasingMode
ûû@ J
.
ûûJ K
	EaseInOut
ûûK T
}
üü 
}
†† 
;
†† 
grid
¢¢ 
.
¢¢ 
BeginAnimation
¢¢ 
(
¢¢  
	UIElement
¢¢  )
.
¢¢) *
OpacityProperty
¢¢* 9
,
¢¢9 :
fadeOut
¢¢; B
)
¢¢B C
;
¢¢C D
await
££ 
Task
££ 
.
££ 
Delay
££ 
(
££ 
$num
££  
)
££  !
;
££! "
}
§§ 	
}
¶¶ 
}ßß §5
C:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\LoginPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
	LoginPage "
:# $
Page% )
,) *!
ILoginManagerCallback+ @
{ 
private 
LoginManagerClient "
_loginClient# /
;/ 0
private   
LoadingPopUpWindow   "
_loadingPopUpWindow  # 6
;  6 7
public"" 
	LoginPage"" 
("" 
)"" 
{## 	
InitializeComponent$$ 
($$  
)$$  !
;$$! "
}%% 	
public'' 
void'' 
LoginResponse'' !
(''! "
bool''" &
success''' .
,''. /
string''0 6
message''7 >
)''> ?
{(( 	
Application)) 
.)) 
Current)) 
.))  

Dispatcher))  *
.))* +
Invoke))+ 1
())1 2
())2 3
)))3 4
=>))5 7
{** 
_loadingPopUpWindow++ #
?++# $
.++$ %
StopLoadingAndClose++% 8
(++8 9
)++9 :
;++: ;
if,, 
(,, 
success,, 
),, 
{-- 
CurrentSession.. "
..." #
CurrentUserNickname..# 6
=..7 8
NicknameTextBox..9 H
...H I
Text..I M
...M N
Trim..N R
(..R S
)..S T
;..T U
new// 
SimplePopUpWindow// )
(//) *
Global//* 0
.//0 1
SuccessLabel//1 =
,//= >
message//? F
)//F G
.//G H

ShowDialog//H R
(//R S
)//S T
;//T U
NavigationService00 %
?00% &
.00& '
Navigate00' /
(00/ 0
new000 3
MainMenuPage004 @
(00@ A
)00A B
)00B C
;00C D
}11 
else22 
{33 
new44 
SimplePopUpWindow44 )
(44) *
Global44* 0
.440 1
UnsuccessfulLabel441 B
,44B C
message44D K
)44K L
.44L M

ShowDialog44M W
(44W X
)44X Y
;44Y Z
}55 
}66 
)66 
;66 
}77 	
private99 
void99 
ClickLoginButton99 %
(99% &
object99& ,
sender99- 3
,993 4
RoutedEventArgs995 D
e99E F
)99F G
{:: 	
SoundManager;; 
.;; 
	PlayClick;; "
(;;" #
);;# $
;;;$ %
string<< 
nickname<< 
=<< 
NicknameTextBox<< -
.<<- .
Text<<. 2
.<<2 3
Trim<<3 7
(<<7 8
)<<8 9
;<<9 :
string== 
password== 
=== 
PasswordField== +
.==+ ,
Password==, 4
;==4 5
var>> 
credentials>> 
=>> 
new>> !
AuthCredentials>>" 1
{?? 
Nickname@@ 
=@@ 
nickname@@ #
,@@# $
PasswordAA 
=AA 
passwordAA #
}BB 
;BB 
ListDD 
<DD 
stringDD 
>DD 
errorsDD 
=DD  !
UserValidatorDD" /
.DD/ 0
ValidateLoginDD0 =
(DD= >
credentialsDD> I
)DDI J
;DDJ K
ifEE 
(EE 
errorsEE 
.EE 
CountEE 
>EE 
$numEE  
)EE  !
{FF 
stringGG 
messageGG 
=GG  
$strGG! %
+GG& '
stringGG( .
.GG. /
JoinGG/ 3
(GG3 4
$strGG4 :
,GG: ;
errorsGG< B
)GGB C
;GGC D
newHH 
SimplePopUpWindowHH %
(HH% &
GlobalHH& ,
.HH, -
WarningLabelHH- 9
,HH9 :
messageHH; B
)HHB C
.HHC D

ShowDialogHHD N
(HHN O
)HHO P
;HHP Q
returnII 
;II 
}JJ 
tryLL 
{MM 
_loadingPopUpWindowNN #
=NN$ %
newNN& )
LoadingPopUpWindowNN* <
(NN< =
)NN= >
{OO 
OwnerPP 
=PP 
WindowPP "
.PP" #
	GetWindowPP# ,
(PP, -
thisPP- 1
)PP1 2
}QQ 
;QQ 
_loadingPopUpWindowRR #
.RR# $
ShowRR$ (
(RR( )
)RR) *
;RR* +
varSS 
contextSS 
=SS 
newSS !
InstanceContextSS" 1
(SS1 2
thisSS2 6
)SS6 7
;SS7 8
_loginClientTT 
=TT 
newTT "
LoginManagerClientTT# 5
(TT5 6
contextTT6 =
)TT= >
;TT> ?
_loginClientUU 
.UU 
LoginUU "
(UU" #
credentialsUU# .
)UU. /
;UU/ 0
}VV 
catchWW 
(WW 
	ExceptionWW 
exWW 
)WW  
{XX 
_loadingPopUpWindowYY #
?YY# $
.YY$ %
StopLoadingAndCloseYY% 8
(YY8 9
)YY9 :
;YY: ;
newZZ 
SimplePopUpWindowZZ %
(ZZ% &
GlobalZZ& ,
.ZZ, -
UnsuccessfulLabelZZ- >
,ZZ> ?
exZZ@ B
.ZZB C
MessageZZC J
)ZZJ K
.ZZK L

ShowDialogZZL V
(ZZV W
)ZZW X
;ZZX Y
}[[ 
}\\ 	
private^^ 
void^^ 
ClickCancelButton^^ &
(^^& '
object^^' -
sender^^. 4
,^^4 5
RoutedEventArgs^^6 E
e^^F G
)^^G H
{__ 	
SoundManager`` 
.`` 
	PlayClick`` "
(``" #
)``# $
;``$ %
NavigationServiceaa 
?aa 
.aa 
GoBackaa %
(aa% &
)aa& '
;aa' (
}bb 	
privatedd 
voiddd  
SignUpLabelMouseDowndd )
(dd) *
objectdd* 0
senderdd1 7
,dd7 8 
MouseButtonEventArgsdd9 M
eddN O
)ddO P
{ee 	
NavigationServiceff 
?ff 
.ff 
Navigateff '
(ff' (
newff( +
RegisterPageff, 8
(ff8 9
)ff9 :
)ff: ;
;ff; <
}gg 	
}hh 
}ii “
C:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\LobbyPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
	LobbyPage "
:# $
Page% )
{ 
public 
	LobbyPage 
( 
) 
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
} 
} ñD
ÖC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\EditProfilePage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
EditProfilePage (
:) *
Page+ /
,/ 0'
IProfileEditManagerCallback1 L
{ 
private $
ProfileEditManagerClient (
_profileEditClient) ;
;; <
private   
readonly   
ClientProfileData   *
_currentProfile  + :
;  : ;
private!! 
LoadingPopUpWindow!! "
_loadingPopUpWindow!!# 6
;!!6 7
public## 
EditProfilePage## 
(## 
ClientProfileData## 0
currentProfile##1 ?
)##? @
{$$ 	
InitializeComponent%% 
(%%  
)%%  !
;%%! "
_currentProfile&& 
=&& 
currentProfile&& ,
??&&- /
new&&0 3
ClientProfileData&&4 E
(&&E F
)&&F G
;&&G H
LoadProfileData'' 
('' 
)'' 
;'' 
}(( 	
public** 
void** !
ProfileUpdateResponse** )
(**) *
bool*** .
success**/ 6
,**6 7
string**8 >
message**? F
)**F G
{++ 	
Application,, 
.,, 
Current,, 
.,,  

Dispatcher,,  *
.,,* +
Invoke,,+ 1
(,,1 2
(,,2 3
),,3 4
=>,,5 7
{-- 
_loadingPopUpWindow.. #
?..# $
...$ %
StopLoadingAndClose..% 8
(..8 9
)..9 :
;..: ;
if// 
(// 
success// 
)// 
{00 
new11 
SimplePopUpWindow11 )
(11) *
Global11* 0
.110 1
SuccessLabel111 =
,11= >
message11? F
)11F G
.11G H

ShowDialog11H R
(11R S
)11S T
;11T U
NavigationService22 %
?22% &
.22& '
Navigate22' /
(22/ 0
new220 3
YourProfilePage224 C
(22C D
)22D E
)22E F
;22F G
}33 
else44 
{55 
new66 
SimplePopUpWindow66 )
(66) *
Global66* 0
.660 1
UnsuccessfulLabel661 B
,66B C
message66D K
)66K L
.66L M

ShowDialog66M W
(66W X
)66X Y
;66Y Z
}77 
}88 
)88 
;88 
}99 	
private;; 
void;; 
ClickSaveButton;; $
(;;$ %
object;;% +
sender;;, 2
,;;2 3
RoutedEventArgs;;4 C
e;;D E
);;E F
{<< 	
try== 
{>> 
SoundManager?? 
.?? 
	PlayClick?? &
(??& '
)??' (
;??( )
var@@ 
updatedProfile@@ "
=@@# $
new@@% (
ClientProfileData@@) :
{AA 
NicknameBB 
=BB 
_currentProfileBB .
.BB. /
NicknameBB/ 7
,BB7 8
FullNameCC 
=CC 
FullNameTextBoxCC .
.CC. /
TextCC/ 3
.CC3 4
TrimCC4 8
(CC8 9
)CC9 :
,CC: ;
EmailDD 
=DD 
EmailTextBoxDD (
.DD( )
TextDD) -
.DD- .
TrimDD. 2
(DD2 3
)DD3 4
,DD4 5
PasswordEE 
=EE 
PasswordFieldEE ,
.EE, -
PasswordEE- 5
,EE5 6
FacebookUrlFF 
=FF  !
FacebookLinkTextBoxFF" 5
.FF5 6
TextFF6 :
.FF: ;
TrimFF; ?
(FF? @
)FF@ A
,FFA B
InstagramUrlGG  
=GG! " 
InstagramLinkTextBoxGG# 7
.GG7 8
TextGG8 <
.GG< =
TrimGG= A
(GGA B
)GGB C
,GGC D
	TikTokUrlHH 
=HH 
TikTokLinkTextBoxHH  1
.HH1 2
TextHH2 6
.HH6 7
TrimHH7 ;
(HH; <
)HH< =
}II 
;II 
varKK 
errorsKK 
=KK 
UserValidatorKK *
.KK* +!
ValidateProfileUpdateKK+ @
(KK@ A
updatedProfileKKA O
.KKO P!
ToProfileEditContractKKP e
(KKe f
)KKf g
)KKg h
;KKh i
ifMM 
(MM 
errorsMM 
.MM 
CountMM  
>MM! "
$numMM# $
)MM$ %
{NN 
stringOO 
messageOO "
=OO# $
$strOO% )
+OO* +
stringOO, 2
.OO2 3
JoinOO3 7
(OO7 8
$strOO8 >
,OO> ?
errorsOO@ F
)OOF G
;OOG H
newPP 
SimplePopUpWindowPP )
(PP) *
GlobalPP* 0
.PP0 1
WarningLabelPP1 =
,PP= >
messagePP? F
)PPF G
.PPG H

ShowDialogPPH R
(PPR S
)PPS T
;PPT U
returnQQ 
;QQ 
}RR 
_loadingPopUpWindowTT #
=TT$ %
newTT& )
LoadingPopUpWindowTT* <
(TT< =
)TT= >
{UU 
OwnerVV 
=VV 
WindowVV "
.VV" #
	GetWindowVV# ,
(VV, -
thisVV- 1
)VV1 2
}WW 
;WW 
_loadingPopUpWindowXX #
.XX# $
ShowXX$ (
(XX( )
)XX) *
;XX* +
varZZ 
contextZZ 
=ZZ 
newZZ !
InstanceContextZZ" 1
(ZZ1 2
thisZZ2 6
)ZZ6 7
;ZZ7 8
_profileEditClient[[ "
=[[# $
new[[% ($
ProfileEditManagerClient[[) A
([[A B
context[[B I
)[[I J
;[[J K
var\\ 
contractProfile\\ #
=\\$ %
updatedProfile\\& 4
.\\4 5!
ToProfileEditContract\\5 J
(\\J K
)\\K L
;\\L M
_profileEditClient]] "
.]]" #
UpdateProfileData]]# 4
(]]4 5
contractProfile]]5 D
)]]D E
;]]E F
}^^ 
catch__ 
(__ 
	Exception__ 
)__ 
{`` 
_loadingPopUpWindowaa #
?aa# $
.aa$ %
StopLoadingAndCloseaa% 8
(aa8 9
)aa9 :
;aa: ;
newbb 
SimplePopUpWindowbb %
(bb% &
Globalbb& ,
.bb, -
UnsuccessfulLabelbb- >
,bb> ?
ErrorMessagesbb@ M
.bbM N'
ConnectionErrorMessageLabelbbN i
)bbi j
.bbj k

ShowDialogbbk u
(bbu v
)bbv w
;bbw x
}cc 
}dd 	
privateff 
voidff 
ClickCancelButtonff &
(ff& '
objectff' -
senderff. 4
,ff4 5
RoutedEventArgsff6 E
effF G
)ffG H
{gg 	
SoundManagerhh 
.hh 
	PlayClickhh "
(hh" #
)hh# $
;hh$ %
NavigationServiceii 
?ii 
.ii 
Navigateii '
(ii' (
newii( +
YourProfilePageii, ;
(ii; <
)ii< =
)ii= >
;ii> ?
}jj 	
privatell 
voidll 
LoadProfileDatall $
(ll$ %
)ll% &
{mm 	
NicknameTextBoxnn 
.nn 
Textnn  
=nn! "
_currentProfilenn# 2
.nn2 3
Nicknamenn3 ;
;nn; <
FullNameTextBoxoo 
.oo 
Textoo  
=oo! "
_currentProfileoo# 2
.oo2 3
FullNameoo3 ;
;oo; <
EmailTextBoxpp 
.pp 
Textpp 
=pp 
_currentProfilepp  /
.pp/ 0
Emailpp0 5
;pp5 6
FacebookLinkTextBoxqq 
.qq  
Textqq  $
=qq% &
_currentProfileqq' 6
.qq6 7
FacebookUrlqq7 B
;qqB C 
InstagramLinkTextBoxrr  
.rr  !
Textrr! %
=rr& '
_currentProfilerr( 7
.rr7 8
InstagramUrlrr8 D
;rrD E
TikTokLinkTextBoxss 
.ss 
Textss "
=ss# $
_currentProfiless% 4
.ss4 5
	TikTokUrlss5 >
;ss> ?
}tt 	
}uu 
}vv ç
ïC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\CustomUserControls\LoadingProgressBar.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
CustomUserControls ,
{ 
public 

partial 
class 
LoadingProgressBar +
:, -
UserControl. 9
{ 
private 
bool 

_isRunning 
;  
public 
LoadingProgressBar !
(! "
)" #
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
public 
void 
Start 
( 
) 
{ 	
if   
(   

_isRunning   
)   
{!! 
return"" 
;"" 
}## 

_isRunning$$ 
=$$ 
true$$ 
;$$ 

Visibility%% 
=%% 

Visibility%% #
.%%# $
Visible%%$ +
;%%+ ,
_&& 
=&&  
AnimateProgressAsync&& $
(&&$ %
)&&% &
;&&& '
}'' 	
public)) 
async)) 
void)) 
Stop)) 
()) 
)))  
{** 	

_isRunning++ 
=++ 
false++ 
;++ 
MainProgressBar,, 
.,, 
Value,, !
=,," #
$num,,$ '
;,,' (
await-- 
Task-- 
.-- 
Delay-- 
(-- 
$num--  
)--  !
;--! "

Visibility.. 
=.. 

Visibility.. #
...# $
	Collapsed..$ -
;..- .
}// 	
private11 
async11 
Task11  
AnimateProgressAsync11 /
(11/ 0
)110 1
{22 	
MainProgressBar33 
.33 
Value33 !
=33" #
$num33$ %
;33% &
while44 
(44 

_isRunning44 
&&44  
MainProgressBar44! 0
.440 1
Value441 6
<447 8
$num449 ;
)44; <
{55 
MainProgressBar66 
.66  
Value66  %
+=66& (
$num66) *
;66* +
await77 
Task77 
.77 
Delay77  
(77  !
$num77! $
)77$ %
;77% &
}88 
}99 	
}:: 
};; Â
C:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Utilities\LogManager.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
	Utilities #
{ 
public 

static 
class 

LogManager "
{ 
private 
static 
readonly 
ILog  $
_logger% ,
;, -
static 

LogManager 
( 
) 
{ 	
var 
logRepository 
= 
log4net  '
.' (

LogManager( 2
.2 3
GetRepository3 @
(@ A
AssemblyA I
.I J
GetEntryAssemblyJ Z
(Z [
)[ \
)\ ]
;] ^
XmlConfigurator 
. 
	Configure %
(% &
logRepository& 3
,3 4
new5 8
FileInfo9 A
(A B
$strB N
)N O
)O P
;P Q
_logger 
= 
log4net 
. 

LogManager (
.( )
	GetLogger) 2
(2 3
typeof3 9
(9 :

LogManager: D
)D E
)E F
;F G
if 
( 
! 
	Directory 
. 
Exists !
(! "
$str" (
)( )
)) *
	Directory 
. 
CreateDirectory )
() *
$str* 0
)0 1
;1 2
_logger 
. 
Info 
( 
$str ?
)? @
;@ A
} 	
public!! 
static!! 
void!! 
Info!! 
(!!  
string!!  &
message!!' .
)!!. /
{"" 	
_logger## 
.## 
Info## 
(## 
message##  
)##  !
;##! "
}$$ 	
public&& 
static&& 
void&& 
Warn&& 
(&&  
string&&  &
message&&' .
)&&. /
{'' 	
_logger(( 
.(( 
Warn(( 
((( 
message((  
)((  !
;((! "
})) 	
public++ 
static++ 
void++ 
Error++  
(++  !
string++! '
message++( /
,++/ 0
	Exception++1 :
ex++; =
=++> ?
null++@ D
)++D E
{,, 	
if-- 
(-- 
ex-- 
!=-- 
null-- 
)-- 
_logger.. 
... 
Error.. 
(.. 
$"..  
{..  !
message..! (
}..( )
$str..) ,
{.., -
ex..- /
.../ 0
GetType..0 7
(..7 8
)..8 9
...9 :
Name..: >
}..> ?
$str..? A
{..A B
ex..B D
...D E
Message..E L
}..L M
"..M N
,..N O
ex..P R
)..R S
;..S T
else// 
_logger00 
.00 
Error00 
(00 
message00 %
)00% &
;00& '
}11 	
public33 
static33 
void33 
Debug33  
(33  !
string33! '
message33( /
)33/ 0
{44 	
_logger55 
.55 
Debug55 
(55 
message55 !
)55! "
;55" #
}66 	
}77 
}88 ù
éC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Utilities\ProgressBarWidthConverter.cs
	namespace

 	
UnoLisClient


 
.

 
UI

 
.

 
	Utilities

 #
{ 
public 

class %
ProgressBarWidthConverter *
:+ , 
IMultiValueConverter- A
{ 
public 
object 
Convert 
( 
object $
[$ %
]% &
values' -
,- .
Type/ 3

targetType4 >
,> ?
object@ F
	parameterG P
,P Q
CultureInfoR ]
culture^ e
)e f
{ 	
if 
( 
values 
== 
null 
|| !
values" (
.( )
Length) /
<0 1
$num2 3
||4 6
values 
[ 
$num 
] 
== 
DependencyProperty /
./ 0

UnsetValue0 :
||; =
values 
[ 
$num 
] 
== 
DependencyProperty /
./ 0

UnsetValue0 :
||; =
values 
[ 
$num 
] 
== 
DependencyProperty /
./ 0

UnsetValue0 :
||; =
values 
[ 
$num 
] 
== 
DependencyProperty /
./ 0

UnsetValue0 :
): ;
{ 
return 
$num 
; 
} 
double 
value 
= 
( 
double "
)" #
values# )
[) *
$num* +
]+ ,
;, -
double 
minimum 
= 
( 
double $
)$ %
values% +
[+ ,
$num, -
]- .
;. /
double 
maximum 
= 
( 
double $
)$ %
values% +
[+ ,
$num, -
]- .
;. /
double 
actualWidth 
=  
(! "
double" (
)( )
values) /
[/ 0
$num0 1
]1 2
;2 3
if 
( 
maximum 
== 
minimum "
||# %
actualWidth& 1
==2 4
$num5 6
)6 7
{ 
return   
$num   
;   
}!! 
double## 
ratio## 
=## 
(## 
value## !
-##" #
minimum##$ +
)##+ ,
/##- .
(##/ 0
maximum##0 7
-##8 9
minimum##: A
)##A B
;##B C
return$$ 
Math$$ 
.$$ 
Max$$ 
($$ 
$num$$ 
,$$ 
ratio$$ $
*$$% &
actualWidth$$' 2
)$$2 3
;$$3 4
}%% 	
public'' 
object'' 
['' 
]'' 
ConvertBack'' #
(''# $
object''$ *
value''+ 0
,''0 1
Type''2 6
[''6 7
]''7 8
targetTypes''9 D
,''D E
object''F L
	parameter''M V
,''V W
CultureInfo''X c
culture''d k
)''k l
{(( 	
return)) 
new)) 
object)) 
[)) 
])) 
{))  !
DependencyProperty))" 4
.))4 5

UnsetValue))5 ?
}))@ A
;))A B
}** 	
}++ 
},, å
ÇC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Utilities\BrowserHelper.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
	Utilities #
{ 
public 

static 
class 
BrowserHelper %
{ 
private 
const 
string 

HttpPrefix '
=( )
$str* 3
;3 4
private 
const 
string 
HttpsPrefix (
=) *
$str+ 5
;5 6
public 
static 
void 
OpenUrl "
(" #
string# )
url* -
)- .
{ 	
if 
( 
string 
. 
IsNullOrWhiteSpace )
() *
url* -
)- .
). /
{ 
return 
; 
} 
try 
{ 
if 
( 
! 
url 
. 

StartsWith #
(# $

HttpPrefix$ .
,. /
StringComparison0 @
.@ A
OrdinalIgnoreCaseA R
)R S
&&T V
! 
url 
. 

StartsWith #
(# $
HttpsPrefix$ /
,/ 0
StringComparison1 A
.A B
OrdinalIgnoreCaseB S
)S T
)T U
{ 
url   
=   
HttpsPrefix   %
+  & '
url  ( +
;  + ,
}!! 
var## 
processStartInfo## $
=##% &
new##' *
ProcessStartInfo##+ ;
{$$ 
FileName%% 
=%% 
url%% "
,%%" #
UseShellExecute&& #
=&&$ %
true&&& *
}'' 
;'' 
Process)) 
.)) 
Start)) 
()) 
processStartInfo)) .
))). /
;))/ 0
}** 
catch++ 
(++ 
	Exception++ 
)++ 
{,, 
new-- 
SimplePopUpWindow-- %
(--% &
Global--& ,
.--, -
WarningLabel--- 9
,--9 :
ErrorMessages.. !
...! "(
UnableToOpenLinkMessageLabel.." >
)..> ?
.// 

ShowDialog// 
(//  
)//  !
;//! "
}00 
}11 	
}22 
}33 §
ÉC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\MatchMenuPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
MatchMenuPage &
:' (
Page) -
{ 
public 
MatchMenuPage 
( 
) 
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
private 
void #
CreateMatchButton_Click ,
(, -
object- 3
sender4 :
,: ;
RoutedEventArgs< K
eL M
)M N
{ 	
SoundManager 
. 
	PlayClick "
(" #
)# $
;$ %
NavigationService 
? 
. 
Navigate '
(' (
new( +
GameSettingsPage, <
(< =
)= >
)> ?
;? @
}   	
private"" 
void"" !
JoinMatchButton_Click"" *
(""* +
object""+ 1
sender""2 8
,""8 9
RoutedEventArgs"": I
e""J K
)""K L
{## 	
SoundManager$$ 
.$$ 
	PlayClick$$ "
($$" #
)$$# $
;$$$ %
NavigationService%% 
?%% 
.%% 
Navigate%% '
(%%' (
new%%( +
JoinMatchPage%%, 9
(%%9 :
)%%: ;
)%%; <
;%%< =
}&& 	
}'' 
}(( £
ÉC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\Pages\JoinMatchPage.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
. 
Pages 
{ 
public 

partial 
class 
JoinMatchPage &
:' (
Page) -
{ 
public 
JoinMatchPage 
( 
) 
{ 	
InitializeComponent 
(  
)  !
;! "
} 	
private 
void 
JoinButton_Click %
(% &
object& ,
sender- 3
,3 4
RoutedEventArgs5 D
eE F
)F G
{ 	
SoundManager 
. 
	PlayClick "
(" #
)# $
;$ %
string   
code   
=   
CodeTextBox   %
.  % &
Text  & *
.  * +
Trim  + /
(  / 0
)  0 1
;  1 2
if"" 
("" 
string"" 
."" 
IsNullOrEmpty"" $
(""$ %
code""% )
)"") *
)""* +
{## 

MessageBox$$ 
.$$ 
Show$$ 
($$  
$str$$  <
,$$< =
$str$$> G
,$$G H
MessageBoxButton$$I Y
.$$Y Z
OK$$Z \
,$$\ ]
MessageBoxImage$$^ m
.$$m n
Warning$$n u
)$$u v
;$$v w
return%% 
;%% 
}&& 
if)) 
()) 
code)) 
.)) 
Length)) 
==)) 
$num))  
)))  !
{** 
new++ 
SimplePopUpWindow++ %
(++% &
$str++& /
,++/ 0
$"++1 3
$str++3 K
{++K L
code++L P
}++P Q
$str++Q X
"++X Y
)++Y Z
.++Z [

ShowDialog++[ e
(++e f
)++f g
;++g h
NavigationService,, !
?,,! "
.,," #
Navigate,,# +
(,,+ ,
new,,, /
MatchLobbyPage,,0 >
(,,> ?
),,? @
),,@ A
;,,A B
}-- 
else.. 
{// 

MessageBox00 
.00 
Show00 
(00  
$str00  5
,005 6
$str007 @
,00@ A
MessageBoxButton00B R
.00R S
OK00S U
,00U V
MessageBoxImage00W f
.00f g
Error00g l
)00l m
;00m n
}11 
}22 	
private44 
void44 
CancelButton_Click44 '
(44' (
object44( .
sender44/ 5
,445 6
RoutedEventArgs447 F
e44G H
)44H I
{55 	
SoundManager66 
.66 
	PlayClick66 "
(66" #
)66# $
;66$ %
NavigationService77 
?77 
.77 
Navigate77 '
(77' (
new77( +
MatchMenuPage77, 9
(779 :
)77: ;
)77; <
;77< =
}88 	
}99 
}:: ƒ&
sC:\Users\meler\OneDrive\Escritorio\Tecnolog√≠as para el Desarrollo de Software\Proyecto\UnoLisClient.UI\App.xaml.cs
	namespace 	
UnoLisClient
 
. 
UI 
{ 
public 

partial 
class 
App 
: 
Application *
,* +"
ILogoutManagerCallback, B
{ 
private 
LogoutManagerClient #
_logoutClient$ 1
;1 2
public 
void 
LogoutResponse "
(" #
bool# '
success( /
,/ 0
string1 7
message8 ?
)? @
{ 	
Application 
. 
Current 
.  

Dispatcher  *
.* +
Invoke+ 1
(1 2
(2 3
)3 4
=>5 7
{ 
if   
(   
!   
success   
)   
{!! 
new"" 
SimplePopUpWindow"" )
("") *
Global""* 0
.""0 1
UnsuccessfulLabel""1 B
,""B C
message""D K
)""K L
.""L M

ShowDialog""M W
(""W X
)""X Y
;""Y Z
}## 
}$$ 
)$$ 
;$$ 
}%% 	
	protected'' 
override'' 
void'' 
	OnStartup''  )
('') *
StartupEventArgs''* :
e''; <
)''< =
{(( 	
base)) 
.)) 
	OnStartup)) 
()) 
e)) 
))) 
;)) 
UnoLisClient++ 
.++ 
UI++ 
.++ 
	Utilities++ %
.++% &

LogManager++& 0
.++0 1
Info++1 5
(++5 6
$str++6 S
)++S T
;++T U
var-- 
langCode-- 
=-- 
global-- !
::--! #
UnoLisClient--# /
.--/ 0
UI--0 2
.--2 3

Properties--3 =
.--= >
Settings--> F
.--F G
Default--G N
.--N O
languageCode--O [
;--[ \
if.. 
(.. 
string.. 
... 
IsNullOrWhiteSpace.. )
(..) *
langCode..* 2
)..2 3
)..3 4
{// 
langCode00 
=00 
$str00 "
;00" #
}11 
Thread33 
.33 
CurrentThread33  
.33  !
CurrentUICulture33! 1
=332 3
new334 7
CultureInfo338 C
(33C D
langCode33D L
)33L M
;33M N
Thread44 
.44 
CurrentThread44  
.44  !
CurrentCulture44! /
=440 1
new442 5
CultureInfo446 A
(44A B
langCode44B J
)44J K
;44K L
}55 	
	protected77 
override77 
void77 
OnExit77  &
(77& '
ExitEventArgs77' 4
e775 6
)776 7
{88 	
base99 
.99 
OnExit99 
(99 
e99 
)99 
;99 
LogoutCurrentUser:: 
(:: 
):: 
;::  
};; 	
private== 
void== 
LogoutCurrentUser== &
(==& '
)==' (
{>> 	
try?? 
{@@ 
ifAA 
(AA 
!AA 
stringAA 
.AA 
IsNullOrWhiteSpaceAA .
(AA. /
CurrentSessionAA/ =
.AA= >
CurrentUserNicknameAA> Q
)AAQ R
)AAR S
{BB 
varCC 
contextCC 
=CC  !
newCC" %
InstanceContextCC& 5
(CC5 6
thisCC6 :
)CC: ;
;CC; <
_logoutClientDD !
=DD" #
newDD$ '
LogoutManagerClientDD( ;
(DD; <
contextDD< C
)DDC D
;DDD E
_logoutClientEE !
.EE! "
LogoutAsyncEE" -
(EE- .
CurrentSessionEE. <
.EE< =
CurrentUserNicknameEE= P
)EEP Q
;EEQ R
CurrentSessionGG "
.GG" #
CurrentUserNicknameGG# 6
=GG7 8
nullGG9 =
;GG= >
CurrentSessionHH "
.HH" #"
CurrentUserProfileDataHH# 9
=HH: ;
nullHH< @
;HH@ A
}II 
}JJ 
catchKK 
(KK 
	ExceptionKK 
exKK 
)KK  
{LL 
newMM 
SimplePopUpWindowMM %
(MM% &
GlobalMM& ,
.MM, -
UnsuccessfulLabelMM- >
,MM> ?
exMM@ B
.MMB C
MessageMMC J
)MMJ K
.MMK L

ShowDialogMML V
(MMV W
)MMW X
;MMX Y
}NN 
}OO 	
}PP 
}QQ 