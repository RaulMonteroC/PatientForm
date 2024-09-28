export default class HttpRequest{

  execute(methodType, endpoint, params, body, contentType){
    const headersList = new Headers();
    if(!contentType)
      headersList.append("Content-Type", "application/json");
    
    const options = {
      method: methodType,
      body: body,
      headers:headersList
    };
    
    endpoint = this.#attachQueryParameters(params, endpoint);
    
    return fetch(`${endpoint}`,options);
  }
  
  #attachQueryParameters(params, endpoint){
    if(params != null && typeof params === 'object' && !Array.isArray(params)){
      let queryParams = '?';
      for (const property in params) {
        queryParams += `${property}=${params[property]}&`;
      }
      
      endpoint += queryParams?.slice(0, -1);
    }
    return endpoint;
  }
}