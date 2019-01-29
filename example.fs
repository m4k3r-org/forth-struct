INCLUDE FORTH-STRUCT.FS

STRUCTURE: \ DATE
  CELLFLD FIELDNAME DATE.YEAR
  CELLFLD FIELDNAME DATE.MONTH
  CELLFLD FIELDNAME DATE.DAY
 STRUCTSIZE  DATE.SIZE
 STRUCTALIGN DATE.ALIGN
 STRUCTEMBED DATE.FLD
;STRUCTURE

( year month day date -- )
: DATE.INIT
	>R
	R@ DATE.DAY !
	R@ DATE.MONTH !
	R> DATE.YEAR !
;

( -- )
: DATE.PRINTDELIM [CHAR] / EMIT SPACE ;

( date -- )
: DATE.PRINT
	>R
	R@ DATE.DAY @ .
	DATE.PRINTDELIM
	R@ DATE.MONTH @ .
	DATE.PRINTDELIM
	R> DATE.YEAR @ .
;

STRUCTURE: \ PERSON
  CHARFLD              FIELDNAME PERSON.FIRSTLEN
  CHARFLD 10 ARRAYFLD  FIELDNAME PERSON.FIRST
  CHARFLD              FIELDNAME PERSON.LASTLEN
  CHARFLD 20 ARRAYFLD  FIELDNAME PERSON.LAST
  DATE.FLD             FIELDNAME PERSON.BIRTH
  DATE.FLD             FIELDNAME PERSON.DEATH
 STRUCTSIZE  PERSON.SIZE
 STRUCTALIGN PERSON.ALIGN
 STRUCTEMBED PERSON.FLD
;STRUCTURE

( first firstlen last lastlen [birth YMD] [death YMD] person -- )
: PERSON.INIT
	>R
	R@ PERSON.DEATH DATE.INIT
	R@ PERSON.BIRTH DATE.INIT
	R@ PERSON.LASTLEN C!
	R@ PERSON.LAST R@ PERSON.LASTLEN C@  MOVE
	R@ PERSON.FIRSTLEN C!
	R@ PERSON.FIRST R> PERSON.FIRSTLEN C@ MOVE
;

( person -- )
: PERSON.PRINT
	>R
	R@ PERSON.FIRST R@ PERSON.FIRSTLEN C@ TYPE
	SPACE
	R@ PERSON.LAST R@ PERSON.LASTLEN C@ TYPE
	." , Born "
	R@ PERSON.BIRTH DATE.PRINT
	." , Died "
	R> PERSON.DEATH DATE.PRINT
;

PERSON.SIZE ALLOT HERE VARIABLE JOHN_SMITH
PERSON.SIZE ALLOT HERE VARIABLE HEATHER_FREDRICK

s" John" s" Smith"  1949 10 24  1984 1 20  JOHN_SMITH PERSON.INIT
JOHN_SMITH PERSON.PRINT CR CR

s" Heather" s" Fredrick"  1800 3 14  1990 11 30  HEATHER_FREDRICK PERSON.INIT
1890 HEATHER_FREDRICK PERSON.DEATH DATE.YEAR ! \ Oops, wrong death century.
HEATHER_FREDRICK PERSON.PRINT CR CR
