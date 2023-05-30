#!/bin/bash

# ----------------------------------- Auth Endpoints Test ------------------------------------------------------ #


# Set base URL for API
auth_url="http://localhost:5000/api"

# Register endpoint
register_url="$auth_url/Auth/register"
register_data='{"username": "testuser", "email": "testuser@example.com", "password": "testpassword"}'

echo "Testing register endpoint..."
curl -s -X POST -H "Content-Type: application/json" -d "$register_data" $register_url

# Login endpoint
login_url="$auth_url/Auth/login"
login_data='{"email": "testuser@example.com", "password": "testpassword"}'

echo "Testing login endpoint..."
login_response=$(curl -s -X POST -H "Content-Type: application/json" -d "$login_data" $login_url)
access_token=$(echo $login_response | jq -r '.accessToken')

# Refresh endpoint
refresh_url="$auth_url/Auth/refresh"
refresh_data="{\"accessToken\": \"$access_token\"}"

echo "Testing refresh endpoint..."
refresh_response=$(curl -s -X POST -H "Content-Type: application/json" -d "$refresh_data" $refresh_url)
new_access_token=$(echo $refresh_response | jq -r '.accessToken')

# Set Authorization header for API requests
authorization_header="Authorization: Bearer $new_access_token"

# Create booking endpoint
booking_url="$auth_url/Booking"
booking_data='{"movieId": 1, "tickets": 2}'

echo "Testing create booking endpoint..."
curl -s -X POST -H "Content-Type: application/json" -H "$authorization_header" -d "$booking_data" $booking_url

# Get bookings endpoint
get_bookings_url="$auth_url/Booking"

echo "Testing get bookings endpoint..."
curl -s -X GET -H "$authorization_header" $get_bookings_url



# ----------------------------------- Booking Endpoints Test ------------------------------------------------------ #
# Set the base URL
BASE_URL="http://localhost:5000/api/booking"

# Get an access token from the authentication server (replace with your own credentials)
TOKEN=$(curl -s -X POST -H "Content-Type: application/json" -d '{"username": "user123", "password": "password123"}' http://localhost:5001/api/auth/login | jq -r '.token')

# Create a booking
curl -s -X POST -H "Content-Type: application/json" -H "Authorization: Bearer $TOKEN" -d '{"movieId": 1, "tickets": 2}' "$BASE_URL" | jq .

# Get all bookings for the current user
curl -s -H "Authorization: Bearer $TOKEN" "$BASE_URL" | jq .
