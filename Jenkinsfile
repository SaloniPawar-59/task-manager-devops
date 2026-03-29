pipeline {
    agent any

    environment {
        AWS_REGION = "ap-south-1"
        ECR_REPO = "304686171763.dkr.ecr.ap-south-1.amazonaws.com/task-manager-api"
        SERVER_IP = "35.154.115.130"
    }

    stages {

        stage('Build Application') {
            steps {
                sh 'dotnet restore'
                sh 'dotnet build --no-restore'
            }
        }

        stage('Build Docker Image') {
            steps {
                sh 'docker build -t task-manager-api .'
            }
        }

        stage('Tag Docker Image') {
            steps {
                sh 'docker tag task-manager-api:latest $ECR_REPO:latest'
            }
        }

        stage('Login to ECR') {
            steps {
                sh '''
                aws ecr get-login-password --region $AWS_REGION \
                | docker login --username AWS --password-stdin 304686171763.dkr.ecr.ap-south-1.amazonaws.com
                '''
            }
        }

        stage('Push to ECR') {
            steps {
                sh 'docker push $ECR_REPO:latest'
            }
        }
        stage('Deploy to EC2') {
            steps {
                sh '''
                ssh ubuntu@$SERVER_IP "
                docker pull $ECR_REPO:latest
                docker stop task-api || true
                docker rm task-api || true
                docker run -d -p 80:80 --name task-api $ECR_REPO:latest
                "
                '''
            }

    }
}