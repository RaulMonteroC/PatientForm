import DataTable from "../components/DataTable";

import {useEffect, useState} from "react";
import PatientApi from "../Api/PatientApi";

const columns = [
  { id: 'name', label: 'Name', minWidth: 100 },
  { id: 'lastName', label: 'Last Name', minWidth: 100 },
  { id: 'phoneNumber', label: 'Phone Number', minWidth: 100 },
  { id: 'email', label: 'Email', minWidth: 100 }
];

const patientApi = new PatientApi();

export default function PatientDataTable() {
  const[data, setData] = useState([]);
  const[pageSize, setPageSize] = useState(5);
  const[page, setPage] = useState(1);

  useEffect(() => {
    patientApi.getAll({
      pageSize: pageSize,
      pageNumber:page
    }).then(response => response.json())
      .then(result => {
        setData(result);
      });
  }, [pageSize, page]);
  
  return <DataTable columns={columns} 
                    rows={data}
                    pageSize={pageSize}
                    availablePageSizes={[5, 10, 20, 30]}
                    onPageSizeChanged={setPageSize} 
                    onPageChanged={setPage}/>
}