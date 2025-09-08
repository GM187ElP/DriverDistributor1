#!/bin/bash

# Names
IMAGE_NAME="driverdistributor"
CONTAINER_NAME="driverdistributor"
SQL_CONTAINER="sqlserver"

# Function to check if a Docker image exists
image_exists() {
    sudo docker images -q $1 > /dev/null 2>&1
    return $?
}

# Function to check if a Docker container exists
container_exists() {
    sudo docker ps -a --format '{{.Names}}' | grep -w $1 > /dev/null 2>&1
    return $?
}

# ---------- Restart existing containers ----------
if container_exists $CONTAINER_NAME && image_exists $IMAGE_NAME; then
    echo "Restarting existing containers..."
    sudo docker stop $CONTAINER_NAME
    sudo docker stop $SQL_CONTAINER

    sudo docker start $SQL_CONTAINER
    sudo docker start $CONTAINER_NAME
    echo "Containers restarted!"
    exit 0
fi

# ---------- Rebuild and run container ----------
echo "Building new image and running container..."

# Stop and remove old app container if exists
if container_exists $CONTAINER_NAME; then
    echo "Stopping and removing old container..."
    sudo docker stop $CONTAINER_NAME
    sudo docker rm $CONTAINER_NAME
fi

# Remove old image if exists
if image_exists $IMAGE_NAME; then
    echo "Removing old image..."
    sudo docker rmi $IMAGE_NAME
fi

# Build new image
echo "Building new image..."
sudo docker build -t $IMAGE_NAME .

# Ensure SQL Server is running
if ! sudo docker ps --format '{{.Names}}' | grep -w $SQL_CONTAINER > /dev/null; then
    echo "Starting SQL Server container..."
    sudo docker start $SQL_CONTAINER
fi

# Run new app container (fixed: added sudo)
echo "Running new container..."
sudo docker run -d --name $CONTAINER_NAME -p 5000:8080 -v driverdata:/app/data $IMAGE_NAME

echo "DriverDistributor container is up and running!"
