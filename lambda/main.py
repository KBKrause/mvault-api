import json
import boto3

def lambda_handler(event, context):
    print('Hello!')

    s3 = boto3.client('s3')
    bucket = 'mvault-api'
    key = 'cloud-kicks.png'

    try:
        data = s3.get_object(Bucket=bucket, Key=key)
        json_data = data['Body'].read()

        return json_data

    except Exception as e:
        print(e)
        raise e

    # TODO implement
    return {
        'statusCode': 200,
        'body': json.dumps('Hello from Lambda!')
    }
