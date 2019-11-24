# Default Store Template

## Structure

--MAIN
	- _included
		- site-structure ( structure of the site )
			- main-blocks ( main building blocks )
			- building-blocks ( pages elemnts )
		page-structure.html ( file that creates includes links based on parameters passed )
	- _layouts
	- _sass
	- assets
	- _data
		- site-structure ( contains props for all page elements stored in _building-blocks )
		- menu-display ( elements to display in menus )
	index.md ( main page )
	about.md ( main page )

## How template works
	- Starting at index
		- including page-structure with parameter = to data/site-structure object
		- included page may contain include with parameters to other data/site-structure objects
	- page-structure.html determens based on param what page to load