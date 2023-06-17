//
//  MyButton.swift
//  Pomodoro
//
//  Created by Kagan Kuscu on 7.06.23.
//

import SwiftUI

struct MyButton: View {
    let title: String
    let action: () -> Void
    
    var body: some View {
        Button {
            action()
        } label: {
            Text(title)
        }
        .foregroundColor(.white)
        .font(.title)
        .bold()
        .frame(width: 100, height: 60)
//        .padding(30)
        .background(Color("ButtonColor"))
        .cornerRadius(20)
    }
}

struct MyButton_Previews: PreviewProvider {
    static var previews: some View {
        MyButton(title: String(localized: "start-string")) {
            // action
        }
    }
}
