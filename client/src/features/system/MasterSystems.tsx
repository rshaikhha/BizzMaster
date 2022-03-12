import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import SimpleTable from "../shared/SimpleTable";
export default function MasterSystems() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.basics.masterSystems().then((res) => setList(res))
    }, [])

    const title = 'Master Systems';
    const columns = [
        { header: 'Title', accessor: 'title'},
    ];

    

    const props = {list, title, columns}
    return (
        <SimpleTable {...props} />

    )


}