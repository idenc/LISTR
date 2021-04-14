# LISTR README
# Group 1: Vikram Bawa, Iden Craven, Gurnoor Aujla, Asad Choudhry, Arthur Franca 
## Configuration
Visual Studio 2019 and the .NET desktop development workload must be installed to build LISTR.
## Building and Running
 Double click the LISTR.sln file to open the project in Visual Studio then click the start button in the top toolbar to build and launch the application. If an error is met try clicking the build dropdown on the options bar, then clean solution and click start again. 

## Walkthrough

### Homepage
The first page that open when the application is launched is the homepage. From the homepage you can search for houses, go to the My LISTR page, register for an account, or login.
![homepage](https://i.imgur.com/4c5I6bG.png)

#### Registration
 You can create an account by clicking the register button, and a popup containing a registration form will open. You can enter in your account information and click register and your details will be saved in a cloud database (in plain-text so put throwaway info). Once you click register you will be automatically logged in. Now, the only functional purpose accounts serve is to check if you are a realtor or not. To be considered a realtor, your account must be manually marked as such in the database. To access the realtor menus login to a premade account which will be given below.
![Registration](https://i.imgur.com/7lQOlCA.png)
#### Login
If you are logged in, click the logout button. Then enter "realtor_guy" (without quotes) as the username and "123" (without quotes) as the password and hit enter or click login to login as a realtor. Now the realtor dashboard can be accessed by clicking the dashboard button below the logout button. This will discussed later.
 
 #### Searching
 To search, simply type your search term into the search bar and hit enter or click search. All of the listings are stored in a cloud database and are searched using this search bar by their address, city, or province. There are five listings in the database currently. To see all of these listings search "Calgary" as they are located in Calgary.

### Browsing Page
On the browsing page, going from left to right on the top bar, the LISTR logo can be clicked on to go back to the home page, the My LISTR button will take you to the My LISTR page, which will be discussed later, and the search bar on the top right displays what you searched for, and can be used to start a new search.

For the content of the browsing page, pictures of the house are displayed on the left and any of the pictures in the bottom row of the house can be clicked on to be displayed larger. On the right, information about the house is displayed. Below the info is the favourite button on the left, the skip button in the middle, and the disliked button on the right. These buttons can be clicked to move the house into their respective list in My LISTR and advance to the next house. Alternatively, the arrows can used as hotkeys. The left arrow, triggers favourite, the right arrow triggers dislike, and the up or down arrows keys triggers skip. To the right of those buttons is the Contact Realtor button, which will open a popup allowing the user to send a message with their contact info to the realtor. However, this form does not actually do anything. 
![Browsing page](https://i.imgur.com/1SQid93.png)
### My LISTR page
Clicking on the My LISTR button on the home page or the browsing page will bring you to the My LISTR page. Here the houses that you have favourited, skipped, or disliked can be viewed and interacted with. Once again, clicking the LISTR logo at the top left will bring you back to the homepage, and the back button will return you to whatever page you were on previously. The three tabs near the top can be clicked to access their respective lists. On each of the listings there are four buttons. The view details button can be used to see the full information about the house, the contact realtor button brings up the same mentioned previously, and the two move buttons will move the listing into either of the two lists it is not already in. The search listings bar allows you to search through the listings in the tab that you are currently on by address, city, or province. The reset button will remove all listings in all tabs allowing the user to start fresh. The filters button will bring up a popup to apply filters to the current list. However, this popup does not actually work and is hardcoded to bring up two specific houses with specific filters upon clicking confirm. A remove all filters button appears as well, but has no functionality. Once filter's confirm button is clicked the favourited listings are permanently changed, until the application is relaunched. When the application is closed all favourited, skipped, and disliked listings are reset, i.e. they are stored in memory. 
![My LISTR page](https://i.imgur.com/fp2a3Od.png)
### Realtor Dashboard Page
When logged in as a realtor, clicking the dashboard button on the homepage will bring you to the realtor dashboard. From here, once again, the LISTR logo at the top left will bring you to the homepage, as well as the home button. The tabs Active Listings and Sold listings will allow you to swap between two lists of houses. However, this is hardcoded so that the active listings are all listings except the last one and the sold listings is just the last listing added to the database. The search bar allows for searching by address, city, or province. Each of the listings has two buttons, the edit button brings you to a page that allows you to edit the listing details, and the delete button deletes the listing from the database permanently. To add a new listing , the add listing button can be clicked which will bring you to the Add Listing page.
![Realtor Dashboard](https://i.imgur.com/uUHJe5S.png)
### Add Listing Page
Here information for a listing can be added. The listing type (For sale or rental), Name(s) of owners, Price in CA$, and the address details must be filled out before posting the listing. Up to 5 photos can be uploaded by clicking the green plus button. House tags can be added by typing in a comma separated list of tags. The rest of the fields are pretty self explanatory. The cancel button can be clicked to cancel creating this listing and bring you back to the realtor listings page, the preview button brings you to the preview page, and the post button will upload the listing to the database and will be added to the active listings. 
![Add Listing](https://i.imgur.com/bQOow5H.png)
### Preview Page
The preview page allows realtors to see what their listing will look like to regular users before they post it. The favourite/dislike/skip buttons are disabled and the top bar now only contains a cancel preview button which will bring the realtor back to the add listing page they were editing and the post button allows the realtor to directly post the listing from the preview page.
![Preview Page](https://i.imgur.com/mhZ41z8.png)
