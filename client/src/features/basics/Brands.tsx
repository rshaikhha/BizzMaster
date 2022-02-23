import { useEffect, useState } from "react";
import agent from "../../app/api/agent";
import SimpleTable from "../shared/SimpleTable";
export default function Brands() {


    const [list, setList] = useState<any[]>([]);

    useEffect(() => {
        agent.basics.brands().then((res) => setList(res))
    }, [])

    const title = 'Brands';
    const columns = [
        { header: 'Name', accessor: 'title'},
        { header: 'Country', accessor: 'countryName'}

    ];



    const props = {list, title, columns}
    return (
        <SimpleTable {...props} />

    )


}