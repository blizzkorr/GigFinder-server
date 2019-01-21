# GigFinder Server

## REST-Interface

URL: https://gigfinder.azurewebsites.net/api/...
Authorization token (Google id token) must be always sent in the “Authorization” header.
For current test purposes, this feature is disabled.

### .../login 
	GET /login
	  checks if the current GoogleUser already has an account

### .../artists 
	GET /artists
	  get current authenticated artist profile
	GET /artists/3
	  get artist profile with id 3
	POST /artists
	  create new artist profile for current authenticated user, error if exists
	PUT /artists/3
	  update artist profile of current authenticated user 
	DELETE /artists/3
	  delete artist profile of current authenticated user 
	  
### .../hosts 
	GET /hosts
	  get current authenticated host profile
	GET /hosts/3
	  get host profile with id 3
	POST /hosts
	  create new host profile for current authenticated user, error if exists
	PUT /hosts/3
	  update host profile of current authenticated user 
	DELETE /hosts/3
	  update host profile of current authenticated user 

### .../events
	GET /events?location=<(double)latitude,(double)longitude>&radius=<(double)radius>&genre=<(int)id>&host=<(int)id>&artist=<(int)id>
	  get events for a specified location, genre, host or artist
	GET /events/4
	  get event with id 4
	POST /events
	  create new event for current authenticated user, user must be host
	PUT /events/4
	  update a specific event, current authenticated user hast to be owner
	DELETE /events/4
	  delete a specific event, current authenticated user hast to be owner

### .../pictures
	GET /pictures?event=<(int)id>&host=<(int)id>&artist=<(int)id>
	  get pictures for a specified event, host or artist
	GET /pictures/4
	  get picture with id 4
	POST /pictures
	  create new picture for current authenticated user
	PUT /pictures/4
	  update a specific picture, current authenticated user hast to be owner
	DELETE /pictures/4
	  delete a specific picture, current authenticated user hast to be owner

### .../participations
	GET /participations?event=<(int)id>&host=<(int)id>&artist=<(int)id>
	  get participations by event, host or artist
	GET /participations/5
	  get participations by id
	POST /participations
	  create a new participation for the current authenticated user
	PUT /participations/5
	  update a participation for the current authenticated user
	DELETE /participations/5
	  delete a participation by id for the current authenticated user

### .../messages 
	GET /messages?receiver=<(int)id>
	  get all messages by the current authenticated user, additionally by receiver
	GET /messages/6
	  get messages by id for the current authenticated user
	POST /messages 
	  create a new message for the current authenticated user

### .../reviews
	GET /reviews?host=<(int)id>&artist=<(int)id>
	  get reviews by host or artist
	GET /reviews/7
	  get review by id
	POST /reviews
	  create a new review by the current authenticated user
	PUT /reviews/7
	  get reviews by event, host or artist
	DELETE /reviews/7
	  get reviews by event, host or artist

### .../searchrequests
	GET /searchrequests
	  get all searchrequests for the current authenticated artist
	GET /searchrequests/8
	  get a searchrequest by id for the current authenticated artist
	POST /searchrequests
	  create a new searchrequest for the current authenticated artist
	PUT /searchrequests/8
	  update a searchrequest by id for the current authenticated artist
	DELETE /searchreqsuests/8
	  delete a searchrequest by id for the current authenticated artist

### .../favorites 
	GET /favorites
	  get the favorites for the current authenticated artist
	GET /favorites/3
	  get favorite by id for the current authenticated artist
	POST /favorites
	  create new favorite for the current authenticated artist
	PUT /favorites/3
	  update favorite by id for the current authenticated artist
	DELETE /favorites/3
	  delete favorite by id for the current authenticated artist
	  
### .../genres 
	GET /genres
	  get all genres
	GET /genres/6
	  get genre by id
	  
### .../socialmedias 
	GET /socialmedias
	  get all socialmedias
	GET /socialmedias/6
	  get socialmedia by id
