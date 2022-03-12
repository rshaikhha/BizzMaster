import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import SimpleTable from "../shared/SimpleTable";
export default function UsageTypes() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.basics.usageTypes().then((res) => setList(res))
    }, [])

    const title = 'Usage Types';
    const columns = [
        { header: 'Title', accessor: 'title'},
    ];

    

    const props = {list, title, columns}
    return (
        <SimpleTable {...props} />

    )


}