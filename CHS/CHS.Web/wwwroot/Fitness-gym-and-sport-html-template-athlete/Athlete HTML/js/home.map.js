(function($) {
  "use strict";

// How you would like to style the map. 
// This is where you would paste any style found on Snazzy Maps.
var styles =  [{"featureType":"landscape","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"transit","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"poi","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"water","elementType":"labels","stylers":[{"visibility":"off"}]},{"featureType":"road","elementType":"labels.icon","stylers":[{"visibility":"off"}]},{"stylers":[{"hue":"#00aaff"},{"saturation":-100},{"gamma":2.15},{"lightness":12}]},{"featureType":"road","elementType":"labels.text.fill","stylers":[{"visibility":"on"},{"lightness":24}]},{"featureType":"road","elementType":"geometry","stylers":[{"lightness":57}]}]

// When the window has finished loading create our google map below
google.maps.event.addDomListener(window, 'load', init);        

function init() {
	$('.map-frame-event').each(function(){
		var zoom = parseInt($(this).find('.map-zoom').val());
		var lat = $(this).find('.map-lat').val();
		var lng = $(this).find('.map-lng').val();
		var iconImg = $(this).find('.map-icon-img').val();
		var iconTitle = $(this).find('.map-icon-title').val();
		
		var position = new google.maps.LatLng(lat, lng);
		
		// Basic options for a simple Google Map
		// For more options see: https://developers.google.com/maps/documentation/javascript/reference#MapOptions
		var mapOptions = {
			// How zoomed in you want the map to start at (always required)
			zoom: zoom,
			// The latitude and longitude to center the map (always required)
			center: position, // New York
			styles: styles
		};

		// Get the HTML DOM element that will contain your map 
		// We are using a div with id="map" seen below in the <body>
		var mapElement = this;

		// Create the Google Map using our element and options defined above
		var map = new google.maps.Map(mapElement, mapOptions);

		// Let's also add a marker while we're at it
		var marker = new google.maps.Marker({
			position: position,
			map: map,
			title: iconTitle,
			icon: iconImg
		});
	
		$(this).parents('.experience-spoiler').click(function(){
			google.maps.event.trigger(map, 'resize');
			map.setCenter(position);
		})

	});
	
}
})(jQuery);