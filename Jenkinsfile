pipeline {
    agent any

    environment {
        AWS_REGION = "ap-south-1"
        ECR_REPO = "304686171763.dkr.ecr.ap-south-1.amazonaws.com/task-manager-api"
        EC2_TAG = "task-api-server"
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

        stage('Get EC2 Public IP') {
            steps {
                script {
                    env.SERVER_IP = sh(
                        script: """
                        aws ec2 describe-instances \
                        --region ap-south-1 \
                        --filters "Name=tag:Name,Values=$EC2_TAG" \
                        "Name=instance-state-name,Values=running" \
                        --query "Reservations[*].Instances[*].PublicIpAddress" \
                        --output text
                        """,
                        returnStdout: true
                    ).trim()
                }
            }
        }

        stage('Deploy to EC2 Server') {
            steps {
                sh '''
                ssh -o StrictHostKeyChecking=no ubuntu@$SERVER_IP "
                docker pull $ECR_REPO:latest
                docker stop task-api || true
                docker rm task-api || true
                docker run -d  --restart always -p 5000:80 --name task-api $ECR_REPO:latest
                "
                '''
            }
        }

    }
}