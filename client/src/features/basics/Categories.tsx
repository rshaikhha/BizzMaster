import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import SimpleTable from "../shared/SimpleTable";
export default function Categories() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.basics.categories().then((res) => setList(res))
    }, [])

    const title = 'Vehicle Brands';
    const columns = [
        { header: 'Name', accessor: 'title'},
        { header: 'Country', accessor: 'countryName'}

    ];

    

    const props = {list, title, columns}
    return (
        <SimpleTable {...props} />

    )


}