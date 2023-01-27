import React, { useEffect, useState } from 'react'
import { fetchBooks } from '../../api/index';
import { Table, Space } from 'antd';
import type { ColumnsType, TableProps } from 'antd/es/table';
import { Book } from '../../common/interface';


const columns: ColumnsType<Book> = [
  {
    title: 'Title',
    dataIndex: 'title',
    width: '15%',
  },
  {
    title: 'publisher',
    dataIndex: 'publisher',
    width: '15%',
  },
  {
    title: 'Published At',
    dataIndex: 'dateOfPublication',
    render: ((date:string) => getFullDate(date)),
    width: '15%',
  },
  {
    title: 'Status',
    dataIndex: 'isBorrowed',
    render: (text, record) => {
        return {
            props: {
                style: {color: !record.isBorrowed ? "green" : "red" }
            },
            children: <div>{!text? 'Available':'Unavailable'}</div>
        }
    },
    width: '15%',
  },
  {
    title: '',
    dataIndex: 'isBorrowed',
    render: (_, record) => (
      <Space size="middle" >
        {!record.isBorrowed? <a style={{color:"orange"}}>Borrow</a>: <></>}
      </Space>
    ),
    width: '20%',
  }
];

const getFullDate = (date: string): string => {
    const dateAndTime = date.split('T');

    return dateAndTime[0].split('-').reverse().join('-');
};


const onChange: TableProps<Book>['onChange'] = (pagination, filters, sorter, extra) => {
  console.log('params', pagination, filters, sorter, extra);
};

const App: React.FC = () => {

    const [bookData, setBookData] = useState<Book[]>([]);

    useEffect(() => {
        fetchBooks().then(data => {setBookData(data.data); console.log(data)});
    }, [])

    return (
        <div>
            <Table columns={columns} dataSource={bookData} onChange={onChange} />
        </div>
        
    )
}


export default App;