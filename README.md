### Correct Directory
Navigate to the directory where the docker-compose.yml and Dockerfile are located. For example:

`cd C:\Programming\TestDelivery`

### Building the Image
Use docker-compose to build the image:

`docker-compose build`

### Running the Container
To run the container with arguments, use:

`docker-compose run delivery_service "1" "2024-10-30 13:20:30" "/app/common/logs/logs.json" "/app/common/data/result.json"`

If the image is already built:

`docker run --rm testdelivery_delivery_service "1" "2024-10-30 13:20:30" "/app/common/logs/logs.json" "/app/common/data/result.json"`