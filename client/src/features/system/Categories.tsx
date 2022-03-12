import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import HierarchialTable from "../shared/HierarchialTable";
import SimpleTable from "../shared/SimpleTable";
export default function Categories() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.basics.categories().then((res) => setList(res))
    }, [])

    const title = 'Categories';
    const columns = [
         
        { header: 'Level', accessor: 'level'},
        { header: 'Code', accessor: 'code'},
        { header: 'Title', accessor: 'title'},
        { header: 'Usage Type', accessor: 'usageType'},
        { header: 'Master System', accessor: 'masterSystem'},
        { header: 'Item Unit', accessor: 'itemUnit'},
        { header: 'Set Unit', accessor: 'setUnit'},
        { header: 'HSCode', accessor: 'hscode'}

    ];

    

    const props = {list, title, columns}
    return (
        <SimpleTable {...props} />

    )


}