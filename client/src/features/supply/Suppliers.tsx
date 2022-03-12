import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import SimpleTable from "../shared/SimpleTable";
export default function Suppliers() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.Suppliers.list().then((res) => setList(res))
    }, [])

    const title = 'Suppliers';
    const columns = [
        { header: 'Title', accessor: 'title'},
        { header: 'Full title', accessor: 'fullTitle'},
        { header: 'Country', accessor: 'country'},
    ];



    const props = {list, title, columns, detailsAddress : "Suppliers"}

    if (list.length == 0) return <Loadingcomponent message='Loading Products ...' />
    
    return (
        <SimpleTable {...props} />

    )


}