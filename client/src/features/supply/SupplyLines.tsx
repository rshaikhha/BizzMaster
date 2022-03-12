import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import Loadingcomponent from "../../app/layout/Loadingcomponent";
import SimpleTable from "../shared/SimpleTable";
export default function SupplyLines() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.Suppliers.lines().then((res) => setList(res))
    }, [])

    const title = 'Supply Lines';
    const columns = [
        { header: 'Title', accessor: 'title'},
        { header: 'Supplier', accessor: 'supplier'},
        { header: 'Planning Type', accessor: 'defaultPlanningType'},
    ];



    const props = {list, title, columns, detailsAddress : "SupplyLineDetails"}

    if (list.length == 0) return <Loadingcomponent />
    
    return (
        <SimpleTable {...props} />

    )


}