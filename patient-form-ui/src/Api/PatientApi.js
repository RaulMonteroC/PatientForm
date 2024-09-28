import HttpRequest from "./HttpRequest";
import HttpMethods from "./HttpMethods";

export default class PatientApi{
  #request = new HttpRequest();
  
  getAll(queryParams){
    return this.#request.execute(HttpMethods.GET, '/api/patient', queryParams);
  }
}