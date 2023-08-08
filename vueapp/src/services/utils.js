import { useToast } from 'vue-toast-notification';

export default function processError(error) {
    let statusCode = error.response.status;
    let headers = error.response.headers;
    let data = error.response.data;
    
    const toast = useToast({
        position: 'bottom',
        duration: 0
    });
    
    if (headers.customerrorresponse) {
        console.error(`Error => Status code ${data.statusCode}\n Message: ${data.Message}`);
        toast.error(`${data.statusCode} - ${data.Message}`);
    }
    else if (statusCode == 400) {
        console.log(error.response);
        toast.error("Bad request - " + data.title);
    }
    else {
        console.error(error.response);
        toast.error(`${statusCode} - ${error.response.statusText} - ${data}`);
    }
}