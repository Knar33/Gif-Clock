Gif Clock

The idea of the GIF Clock is to create an endless streaming GIF with .NET MVC (If possible) which displays the current time. These are the major milestones in order to accomplish this task:

* (Complete) Create a byte stream that can be fed over time through .NET MVC. 
* Create a custom GIF encoder and serve a static GIF file through .NET MVC.
* Split the GIF Encoder into two pieces, and combine with the endless byte stream: The first part will be the static header of the GIF format. The second part will loop infinitely and generate the current frame, then sleep for the remainder of the frame time (1 second).
* Adjust for both time accuracy, as well as concurrency. The time displayed on the GIF clock should not stray more than 1 whole second within a 10 minute window. Many users shoulb be able to access the GIF clock at the same time without affecting performance.

Articles that have been helpful:
* https://www.strathweb.com/2013/01/asynchronously-streaming-video-with-asp-net-web-api/
* https://www.w3.org/Graphics/GIF/spec-gif89a.txt
* http://www.matthewflickinger.com/lab/whatsinagif/bits_and_bytes.asp