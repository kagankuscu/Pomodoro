//
//  ButtonContainer.swift
//  Pomodoro
//
//  Created by Kagan Kuscu on 7.06.23.
//

import SwiftUI

struct ButtonContainer: View {
    @State private var isStart: Bool = false
    
    var body: some View {
        HStack {
            MyButton(title: "Start") {
                // action
                print("Start")
                isStart = true
            }
            MyButton(title: "Skip") {
                // action
            }
            .opacity(isStart ? 1 : 0)
            MyButton(title: "Reset") {
                // action
                print("Reset")
                isStart = false
            }
            .disabled(!isStart)
        }
    }
}

struct ButtonContainer_Previews: PreviewProvider {
    static var previews: some View {
        ButtonContainer()
    }
}
